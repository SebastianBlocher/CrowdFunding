using crowdFunding.Services;
using crowdFunding.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding
{
    public class ProjectService : IProjectService
    {
        private CrowdFundingDbContext context_;
        public ProjectService(CrowdFundingDbContext context)
        {
            context_ = context;
        }
        public Project CreateProject(CreateProjectOption options)
        {
            if (options == null) return null;

            var project = new Project()
            {
                Name = options.Name,
                Description = options.Description,
                Category = options.Category,
                CreatedOn = options.CreatedOn,

            };

            context_.Add(project);
            return context_.SaveChanges() > 0 ? project : null; 
        }

       

        public IQueryable<Project> SearchProject(SearchProjectOption options)
        {
            if (options == null) return null;

            var query = context_
                   .Set<Project>()
                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(c => c.Name == options.Name);

            }
            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(c => c.Description == options.Description);
            }

            if (options.ProjectId != null)
            {
                query = query.Where(c => c.ProjectId == options.ProjectId);
            }


            return query;
        }


        public Project GetProjectByName(string Name)
        {
            if (Name == null)
            {
                return null;
            }

            return context_
                .Set<Project>()
                .Where(c => c.Name == Name)
                .SingleOrDefault();
        }

         public Project UpdateProject(UpdateProjectOption options)
        {
            throw new NotImplementedException();
        }
        

        public IQueryable<Project> GetProjectByCategory(Category Category)
        {

            if (Category == null)
            {
                return null;
            }

            return context_
                .Set<Project>()
                .Where(c => c.Category == Category);
                
        }
    }

}



