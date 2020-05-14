using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace crowdFunding.Services
{
    public interface IProjectCreatorService
    {
        ProjectCreator CreateProjectCreator(CreateProjectCreatorOptions options);
        IQueryable<ProjectCreator> SearchProjectCreator(SearchProjectCreatorOptions options);
        ProjectCreator UpdateProjectCreator(UpdateProjectCreatorOptions options);
    }
}
