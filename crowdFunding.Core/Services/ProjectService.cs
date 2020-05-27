using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class ProjectService : IProjectService
    {
        private CrowdFundingDbContext context_;
        private IUserService userService_;
       

        public ProjectService(CrowdFundingDbContext context, IUserService userService)
        {
            context_ = context;
            userService_ = userService;
        }

       
        public Project CreateProject(CreateProjectOptions options)
        {
            if (options == null || options.UserId == null ||
                options.Name == null || options.Description == null ||
                options.Category == null || options.Category == 0 || options.AmountRequired == null)
            {
                return null;
            }

            var project = new Project()
            {
                Name = options.Name,
                Description = options.Description,
                Category = (Category)options.Category,
                AmountRequired = (decimal)options.AmountRequired,
            };

            var user = userService_
                .GetById(options.UserId)
                .Include(x => x.CreatedProjectsList)
                .SingleOrDefault();

            user.CreatedProjectsList.Add(project);

            return context_.SaveChanges() > 0 ? project : null;
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
                query = query.Where(p => p.ProjectId == options.ProjectId);
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

        public Project UpdateProject(UpdateProjectOptions options)
        {
            if (options == null || options.ProjectId == null)
            {
                return null;
            }

            var project = context_
                .Set<Project>()
                .Where(x => x.ProjectId == options.ProjectId)
                .SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            if (options.Category != null && options.Category != 0)
            {
                project.Category = (Category)options.Category;
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
                project.AmountRequired = (decimal)options.AmountRequired;
            }

            return context_.SaveChanges() > 0 ? project : null;
        }

        public List<int?> TrendingProjects()
        {
            var project = SearchProject(new SearchProjectOptions()).ToList();

            List<Project> SortedList = project.OrderByDescending(p => p.NumberOfBackers).ToList();

            var trendingProjects = new List<int?>();

            for (var i = 0; i <= 4; i++)
            {
                if (SortedList.Count > i)
                {
                    trendingProjects.Add(SortedList.ElementAt(i).ProjectId);
                }
            }

            return trendingProjects;
        }

        public bool DeleteProject(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var project = GetProjectById(id)
                 .Where(p => p.ProjectId == id)
                 .Include(p => p.RewardPackages)
                 .SingleOrDefault();

            if (project == null)
            {
                return false;
            }

            if (project.RewardPackages != null)
            {
                project.RewardPackages.Clear();
            }
            project.IsActive = false;

            context_.Remove(project);

            return context_.SaveChanges() > 0;
        }
    }
}



