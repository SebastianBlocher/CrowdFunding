using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CrowdFundingDbContext context_;
        private readonly IUserService userService_;
       

        public ProjectService(CrowdFundingDbContext context, IUserService userService)
        {
            context_ = context;
            userService_ = userService;
        }

       
        public Result<Project> CreateProject(int userId,
            CreateProjectOptions options)
        {
            var result = new Result<User>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null or empty Name";
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null or empty Description";
            }

            if (options.AmountRequired <= 0 )
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Invalid AnountRequired";
            }

            if ((int)options.Category < 1 || (int)options.Category > 8)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Invalid Category";
            }

            if (options.RewardPackages.Any() == false || options.RewardPackages == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null or empty Description";
            }

            var project = new Project()
            {
                Name = options.Name,
                Description = options.Description,
                Category = options.Category,
                AmountRequired = options.AmountRequired,
            };

            var user = userService_
                .GetById(userId)
                .Include(x => x.CreatedProjectsList)
                .SingleOrDefault();

            user.CreatedProjectsList.Add(project);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Project could not be created");
            }

            return Result<Project>.ActionSuccessful(project);
        }

        public IQueryable<Project> SearchProject(SearchProjectOptions options)
        {
            if (options == null) return null;

            var query = context_
                   .Set<Project>()
                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(p => p.Name == options.Name);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(p => p.Description == options.Description);
            }

            if (options.ProjectId != null)
            {
                query = query.Where(p => p.ProjectId == options.ProjectId.Value);
            }

            if (options.Category != null && options.Category != 0)
            {
                query = query.Where(p => p.Category == options.Category);
            }

            return query;
        }

        public IQueryable<Project> GetProjectById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return context_
                .Set<Project>()
                .Where(p => p.ProjectId == id);
        }

        public Result<Project> UpdateProject(int projectId,
            UpdateProjectOptions options)
        {
            var result = new Result<Project>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var project = context_
                .Set<Project>()
                .Where(x => x.ProjectId == projectId)
                .SingleOrDefault();

            if (project == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Project with id {projectId} was not found";

                return result;
            }

            if (options.Category != null && options.Category > 0 && (int)options.Category < 9)
            {
                project.Category = options.Category.Value;
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                project.Description = options.Description;
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                project.Name = options.Name;
            }

            if (options.AmountRequired != null)
            {
                project.AmountRequired = options.AmountRequired.Value;
            }

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Project could not be updated");
            }

            return Result<Project>.ActionSuccessful(project);
        }

        public List<int?> TrendingProjects()
        {
            var project = SearchProject(new SearchProjectOptions()).ToList();

            List<Project> SortedList = project.OrderByDescending(p => p.NumberOfBackers).ToList();

            var trendingProjetcs = new List<int?>();

            for (var i = 0; i <= 4; i++)
            {
                if (SortedList.Count > i)
                {
                    trendingProjetcs.Add(SortedList.ElementAt(i).ProjectId);
                }
            }

            return trendingProjetcs;
        }
    }
}



