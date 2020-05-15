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

            //------------------check if the project exists through its ID------------------------------------------
            //var project = SearchProjects(new SearchProjectsOptions()
            //{

            //})

            var backedProject = new BackedProjects()
            {
                Amount = options.Amount,
                ProjectId = options.ProjectId,

            };

            //------------------add backedProject to User's BackedProjectsList

            context_.Add(backedProject);

            if (context_.SaveChanges() > 0)
            {
                return backedProject;
            }

            return null;
        }

        public IQueryable<BackedProjects> SearchBackedProjects(SearchBackedProjectsOptions options, int userId)
        {
            //if (options == null)
            //{
            //    return null;
            //}

            var query = context_
                .Set<User>()
                .AsQueryable();

            //if (userId != null)
            //{
            query = query
            .Where(u => u.UserId == userId)
            .Where(u => u.BackedProjectsList.Count() > 0);
            //}

            if ()
            {

            }




            return null;
        }
    }
}
