using crowdFunding.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
   public interface IProjectService
    {
      Project CreateProject(CreateProjectOption options);
      IQueryable<Project> SearchProject(SearchProjectOption options);
      Project UpdateProject(UpdateProjectOption options);
      Project GetProjectByCategory(Category Category);
     Project GetProjectByName(string Name);
    }
}
