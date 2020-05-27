using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IProjectService
    {
        Project CreateProject(CreateProjectOptions options);
        IQueryable<Project> SearchProject(SearchProjectOptions options);
        Project UpdateProject(UpdateProjectOptions options);
        IQueryable<Project> GetProjectById(int? Id);
        List<int?> TrendingProjects();
        bool DeleteProject(int? id);
    }
}
