using crowdFunding.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public interface IProjectService
    {
        Project CreateProject(CreateProjectOptions options);
        IQueryable<Project> SearchProject(SearchProjectOptions options);
        Project UpdateProject(UpdateProjectOptions options);
        IQueryable<Project> GetProjectById(int? Id);
        List<int?> TrendingProjects();
    }
}
