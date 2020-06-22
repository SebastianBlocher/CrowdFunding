using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class ProjectService : IProjectService
    {
        private CrowdFundingDbContext context_;
        private IUserService userService_;
        private IRewardPackageService rewardPackageService_;
        private IVideoService videoService_;
        private IPhotoService photoService_;
        private IPostService postService_;


        public ProjectService(CrowdFundingDbContext context,
            IUserService userService,
            IRewardPackageService rewardPackageService,
            IVideoService videoService,
            IPhotoService photoService,
            IPostService postService)

        {
            context_ = context;
            userService_ = userService;
            rewardPackageService_ = rewardPackageService;
            videoService_ = videoService;
            photoService_ = photoService;
            postService_ = postService;
        }

       
        public Result<Project> CreateProject(CreateProjectOptions options)
        {
            if (options == null)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                return Result<Project>.ActionFailed(
                   StatusCode.BadRequest, "Null or empty Name");
            }

            if (string.IsNullOrWhiteSpace(options.Description))
            {
                return Result<Project>.ActionFailed(
                   StatusCode.BadRequest, "Null or empty Description");
            }

            if (options.AmountRequired <= 0 || options.AmountRequired == null)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "Null or empty AmountRequired");
            }

            if (options.DueTo == null || options.DueTo.CompareTo(options.CreatedOn) <= 0)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "Invalid Due to date");
            }

            if (options.Videos == null || options.Videos.Any() == false)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "No Videos given");
            }

            if (options.Photos == null || options.Photos.Any() == false)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "No Photos given");
            }

            if ((int)options.Category < 1 || (int)options.Category > 8)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "Null or invalid Category");
            }

            var user = userService_
            .GetById(options.UserId)
            .Include(x => x.CreatedProjectsList)
            .SingleOrDefault();            

            if (user == null)
            {
                return Result<Project>.ActionFailed(
                StatusCode.BadRequest, "Invalid User");
            }            

            var project = new Project()
            {
                Name = options.Name,
                Description = options.Description,
                Category = options.Category,
                AmountRequired = options.AmountRequired.Value,
                DueTo = options.DueTo,
                User = user,
               
            }; 

            foreach (var photo in options.Photos)
            {
                var a = new Photo()
                {
                    Url = photo,
                };
                project.Photos.Add(a);
            }

            foreach (var video in options.Videos)
            {
                var v = new Video()
                {
                    Url = video,
                };
                project.Videos.Add(v);
            }
            user.CreatedProjectsList.Add(project);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Project could not be created");
            }

            return Result<Project>.ActionSuccessful(project);
        }

        public IQueryable<Project> SearchProject(SearchProjectOptions options)
        {
            if (options == null) 
            { 
                return null;
            }

            var query = context_
                   .Set<Project>()
                   .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(p => p.Name.Contains(options.Name));
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(p => p.Description == options.Description);
            }

            if (options.ProjectId != null)
            {
                query = query.Where(p => p.ProjectId == options.ProjectId.Value);
            }

            if (options.Category != null && options.Category != 0)
            {
                query = query.Where(p => p.Category == options.Category);
            }

            query = query.Include(p => p.Photos)
            .Include(p => p.Videos)
            .Include(p => p.Posts)
            .Include(p => p.RewardPackages)
            .ThenInclude(p => p.Rewards)
            .Include(p => p.User);

            return query;
        }

        public Project GetProjectById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var project = SearchProject(new SearchProjectOptions()
            {
                ProjectId = id
            })
            .SingleOrDefault();

            return project;
        }

        public Result<Project> UpdateProject(
            UpdateProjectOptions options)
        {
            var result = new Result<Project>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var project = context_
                .Set<Project>()
                .Where(x => x.ProjectId == options.ProjectId)
                .Include(x => x.Photos)
                .Include(x => x.Videos)
                .SingleOrDefault();

            if (project == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Project with id {options.ProjectId} was not found";

                return result;
            }

            if (options.Category != null && options.Category > 0 && (int)options.Category < 9)
            {
                project.Category = options.Category.Value;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                project.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                project.Name = options.Name;
            }

            if (options.AmountRequired != null)
            {
                project.AmountRequired = options.AmountRequired.Value;
            }

            if (options.DueTo != null && options.DueTo.CompareTo(DateTime.Today) > 0)
            {
                project.DueTo = options.DueTo;
            }

            if (options.Photos !=null)
            {
                if (options.Photos.Count > 0 && !string.IsNullOrWhiteSpace(options.Photos[0]))
                {
                        var p = new Photo()
                        {
                            Url = options.Photos[0],
                        };
                        project.Photos.Add(p);
                }
            }

            if (options.Videos != null)
            {
                if (options.Videos.Count > 0 && !string.IsNullOrWhiteSpace(options.Videos[0]))
                {
                    var v = new Video()
                    {
                        Url = options.Videos[0],
                    };
                    project.Videos.Add(v);
                }
            }

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Project>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Project could not be updated");
            }

            return Result<Project>.ActionSuccessful(project);
        }

        public List<int?> TrendingProjects()
        {
            var project = SearchProject(new SearchProjectOptions()).ToList();

            List<Project> SortedList = project.OrderByDescending(p => p.NumberOfBackers).ToList();

            var trendingProjects = new List<int?>();

            for (var i = 0; i <= 4; i++)
            {
                if (SortedList.Count > i)
                {
                    trendingProjects.Add(SortedList.ElementAt(i).ProjectId);
                }
            }

            return trendingProjects;
        }

        public bool DeleteProject(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var project = GetProjectById(id);              

            if (project == null)
            {
                return false;
            }

            foreach (var rewardPackage in project.RewardPackages.ToList())
            {
                if (!rewardPackageService_.RemoveRewardPackage(rewardPackage.RewardPackageId).Data)
                {
                    return false;
                }
            }

            foreach (var photo in project.Photos.ToList())
            {
                if (!photoService_.DeletePhoto(photo.PhotoId).Data)
                {
                    return false;
                }
            }

            foreach (var post in project.Posts.ToList())
            {
                if (!postService_.DeletePost(post.PostsId).Data)
                {
                    return false;
                }
            }

            foreach (var video in project.Videos.ToList())
            {
                if (!videoService_.DeleteVideo(video.VideoId).Data)
                {
                    return false;
                }
            }

            context_.Remove(project);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }


}



