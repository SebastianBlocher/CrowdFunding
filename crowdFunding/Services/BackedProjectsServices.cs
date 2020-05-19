using crowdFunding.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public class BackedProjectsServices : IBackedProjectsService
    {
        private CrowdFundingDbContext context_;
        private IProjectService projectService_;
        private IUserService userService_;

        public BackedProjectsServices(CrowdFundingDbContext context, IUserService userService, IProjectService projectService)
        {
            context_ = context;
            userService_ = userService;
            projectService_ = projectService;
        }

        public BackedProjects CreateBackedProject(CreateBackedProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var user = userService_
                .GetById(options.UserId)
                .Include(x => x.BackedProjectsList)
                .SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            var project = projectService_
                .SearchProject(new SearchProjectOption()
                {
                    ProjectId = options.ProjectId
                })
                .SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            var backedProject = new BackedProjects()
            {
                Amount = options.Amount,
                ProjectId = options.ProjectId,
            };

            user.BackedProjectsList.Add(backedProject);

            return context_.SaveChanges() > 0 ? backedProject : null;
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
                backedProjects = backedProjects.Where(bp => bp.Category == options.Category);
            }
            if (options.ProjectId != null)
            {
                backedProjects = backedProjects.Where(bp => bp.ProjectId == options.ProjectId);
            }
            if (options.BackedFrom != null)
            {
                backedProjects = backedProjects.Where(bp => bp.BackedOn >= options.BackedFrom);
            }
            if (options.BackedTo != null)
            {
                backedProjects = backedProjects.Where(bp => bp.BackedOn <= options.BackedTo);
            }
            if (options.AmountFrom != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Amount >= options.AmountFrom);
            }
            if (options.AmountTo != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Amount <= options.AmountTo);
            }

            return backedProjects;
        }
    }
}
