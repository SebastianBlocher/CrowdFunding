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
        Project GetProjectById(int? Id);
        bool DeleteProject (int? projectId);
        List<int?> TrendingProjects();
    }
}
