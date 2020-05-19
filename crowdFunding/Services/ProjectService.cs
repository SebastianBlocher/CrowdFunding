using crowdFunding.Services;
using crowdFunding.Services.Options;
using System.Data.Entity;
using System.Linq;

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
        public Project CreateProject(CreateProjectOptions options)
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

            if (options.Category != null)
            {
                query = query.Where(p => p.Category == options.Category);

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
                .Where(p => p.Name == Name);
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

                if (options.Category != null)
                {
                    project.Category = (Category)options.Category;
                }

                if (options.Description != null)
                {
                    project.Description = options.Description;
                }

                if (options.Name != null)
                {
                    project.Name = options.Name;
                }

                if (options.Amount != null)
                {
                    project.Amount = (decimal)options.Amount;
                }

                return context_.SaveChanges() > 0 ? project : null;
            }

        public IQueryable<Project> GetProjectByCategory(Category? Category)
        {
            if (Category == null)
            {
                return null;
            }

            return context_
                .Set<Project>()
                .Where(p => p.Category == Category);
        }
    }
}



