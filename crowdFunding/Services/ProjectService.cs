using crowdFunding.Services;
using crowdFunding.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding
{
    public class ProjectService : IProjectService
    {
        private CrowdFundingDbContext context_;
        private IUserService userService;
        public ProjectService(CrowdFundingDbContext context, IUserService userService_)
        {
            context_ = context;
            userService = userService_;
        }
        public Project CreateProject(CreateProjectOption options)
        {
            if (options == null || options.UserId == null ||
                options.Name == null || options.Description == null ||
                options.Category == null)
            {
                return null;
            }

            var project = new Project()
            {
                Name = options.Name,
                Description = options.Description,
                Category = (Category)options.Category,
                CreatedOn = options.CreatedOn,
            };

            var user = userService
                .GetById(options.UserId)
                .Include(x => x.CreatedProjectsList)
                .SingleOrDefault();

            user.CreatedProjectsList.Add(project);

            return context_.SaveChanges() > 0 ? project : null; // auto to kanoyme gia na vrei an yparxoyn allages kai na tis apothikeysei allios na min kanei tpt?
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


        public IQueryable<Project> GetProjectByName(string Name)
        {
            if (Name == null)
            {
                return null;
            }

            return context_
                .Set<Project>()
                .Where(c => c.Name == Name);
        }

        public Project UpdateProject(UpdateProjectOption options)
        {
            throw new NotImplementedException();
        }
        

        public IQueryable<Project> GetProjectByCategory(Category? Category)
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



