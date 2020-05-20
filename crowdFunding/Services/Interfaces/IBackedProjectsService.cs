using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crowdFunding.Services
{
    public interface IBackedProjectsService
    {
        BackedProjects CreateBackedProject(CreateBackedProjectOptions options);
        IQueryable<BackedProjects> SearchBackedProjects(SearchBackedProjectsOptions options);        
    }
}
