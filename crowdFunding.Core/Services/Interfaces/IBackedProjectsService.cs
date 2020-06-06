using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IBackedProjectsService
    {
        Result<BackedProjects> CreateBackedProject(int userId,int projectId, CreateBackedProjectOptions options);               
    }
}
