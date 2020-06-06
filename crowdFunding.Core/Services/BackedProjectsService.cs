using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class BackedProjectsService : IBackedProjectsService
    {
        private CrowdFundingDbContext context_;
        private IProjectService projectService_;
        private IUserService userService_;

        public BackedProjectsService(CrowdFundingDbContext context,
            IUserService userService,
            IProjectService projectService)
        {
            context_ = context;
            userService_ = userService;
            projectService_ = projectService;
        }

        public Result<BackedProjects> CreateBackedProject(
            CreateBackedProjectOptions options)
        {
            if (options == null)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            var user = userService_
            .GetById(options.UserId)
            .Include(x => x.BackedProjectsList)
            .SingleOrDefault();

            if (user == null)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.BadRequest, "Invalid UserId");
            }

            var project = projectService_
                .GetProjectById(options.ProjectId);                

            if (project == null)
            {
                return Result<BackedProjects>.ActionFailed(
                  StatusCode.BadRequest, "Invalid ProjectId");
            }           

            var backedProject = new BackedProjects()
            {
                Amount = options.Amount,
                ProjectId = project.ProjectId,
                Name = project.Name,
                Category = project.Category,
                Description = project.Description,
                Photo = project.Photos.ElementAt(0).Url,
                NumberOfBackers = project.NumberOfBackers,
                ProjectCreatorId = project.UserId,
                ProjectCreatorFirstName = project.User.FirstName ,
                ProjectCreatorLastName = project.User.LastName
            };

            var backedProjectIds = user.BackedProjectsList
                .Select(s => s.ProjectId).ToList();

            if (backedProjectIds.Contains(project.ProjectId))
            {
                project.AmountGathered += options.Amount;               

                context_.SaveChanges();

                return Result<BackedProjects>.ActionFailed(
                    StatusCode.OK,
                    "Reward Package was succesfully purchased");
            }

            project.NumberOfBackers += 1;            

            project.AmountGathered += options.Amount;

            user.BackedProjectsList.Add(backedProject);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<BackedProjects>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Backed Project could not be created");
            }

            return Result<BackedProjects>.ActionSuccessful(backedProject);
        }
    }
}
