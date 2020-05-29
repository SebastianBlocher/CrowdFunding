using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System.Collections.Generic;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IProjectService
    {
        Result<Project> CreateProject(CreateProjectOptions options);
        IQueryable<Project> SearchProject(SearchProjectOptions options);
        Result<Project> UpdateProject(int projectId, UpdateProjectOptions options);
        IQueryable<Project> GetProjectById(int? Id);
        List<int?> TrendingProjects();
        bool DeleteProject(int? id);
    }
}
