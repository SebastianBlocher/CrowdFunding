using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class BackedProjectsService : IBackedProjectsService
    {
        private CrowdFundingDbContext context_;
        private IProjectService projectService_;
        private IUserService userService_;

        public BackedProjectsService(CrowdFundingDbContext context,
            IUserService userService,
            IProjectService projectService)
        {
            context_ = context;
            userService_ = userService;
            projectService_ = projectService;
        }

        public Result<BackedProjects> CreateBackedProject(int userId,
            int projectId,
            CreateBackedProjectOptions options)
        {
            if (options == null)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            var user = userService_
            .GetById(userId)
            .Include(x => x.BackedProjectsList)
            .SingleOrDefault();

            if (user == null)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.BadRequest, "Invalid UserId");
            }

            var project = projectService_
                .GetProjectById(projectId);                

            if (project == null)
            {
                return Result<BackedProjects>.ActionFailed(
                  StatusCode.BadRequest, "Invalid ProjectId");
            }

            var backedProject = new BackedProjects()
            {
                Amount = options.Amount,
                ProjectId = projectId,
                Name = project.Name,
                Category = project.Category,
                Description = project.Description                
            };

            project.NumberOfBackers += 1;
            project.AmountGathered += options.Amount;

            project.AmountGathered += options.Amount;

            user.BackedProjectsList.Add(backedProject);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Backed Project could not be created");
            }

            return Result<BackedProjects>.ActionSuccessful(backedProject);
        }

        public IQueryable<BackedProjects> SearchBackedProjects(SearchBackedProjectsOptions options)
        {
            if (options == null || options.UserId == null)
            {
                return null;
            }

            var user = userService_
                .GetById(options.UserId)
                .Include(c => c.BackedProjectsList)
                .SingleOrDefault();


            if (user == null)
            {
                return null;
            }

            var backedProjects = user.BackedProjectsList.AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                backedProjects = backedProjects.Where(bp => bp.Name == options.Name);
            }
            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                backedProjects = backedProjects.Where(bp => bp.Description == options.Description);
            }
            if (options.Category != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Category == options.Category.Value);
            }
            if (options.ProjectId != null)
            {
                backedProjects = backedProjects.Where(bp => bp.ProjectId == options.ProjectId.Value);
            }
            if (options.BackedFrom != null)
            {
                backedProjects = backedProjects.Where(bp => bp.BackedOn >= options.BackedFrom.Value);
            }
            if (options.BackedTo != null)
            {
                backedProjects = backedProjects.Where(bp => bp.BackedOn <= options.BackedTo.Value);
            }
            if (options.AmountFrom != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Amount >= options.AmountFrom.Value);
            }
            if (options.AmountTo != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Amount <= options.AmountTo.Value);
            }

            return backedProjects;
        }
    }
}
