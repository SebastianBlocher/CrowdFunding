using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IBackedProjectsService
    {
        Result<BackedProjects> CreateBackedProject(int userId,int projectId, CreateBackedProjectOptions options);
        IQueryable<BackedProjects> SearchBackedProjects(SearchBackedProjectsOptions options);        
    }
}
