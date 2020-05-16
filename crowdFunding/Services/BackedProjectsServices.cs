using crowdFunding.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public class BackedProjectsServices : IBackedProjectsService
    {
        private CrowdFundingDbContext context_;

        public BackedProjectsServices(CrowdFundingDbContext context)
        {
            context_ = context;
        }

        public BackedProjects CreateBackedProject(CreateBackedProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }
            IUserService userService = new UserService(context_);
            IProjectService projectService = new ProjectService(context_);

            var user = userService
                .GetById(options.UserId)
                .SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            var project = projectService
                .SearchProject(new SearchProjectOption()
                {
                    ProjectId = options.ProjectId
                })
                .SingleOrDefault();

            if (project == null)
            {
                return null;
            }

            userService.UpdateUser(new UpdateUserOptions()
            {
                UserId = options.UserId,
                NewBackedProjectId = options.ProjectId,
                BackedProjectAmount = options.Amount
            });

            return null;
        }

        public IQueryable<BackedProjects> SearchBackedProjects(SearchBackedProjectsOptions options)
        {
            //if (options == null)
            //{
            //    return null;
            //}

            var user = new UserService(context_).GetById(options.userId).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            var backedProjects = user.BackedProjectsList.AsQueryable();

            if (options.Name != null)
            {
                backedProjects = backedProjects.Where(bp => bp.Name == options.Name);
            }
            if (options.Description != null)
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


            //Console.WriteLine(user.FirstName);

            return backedProjects;
        }
    }
}
