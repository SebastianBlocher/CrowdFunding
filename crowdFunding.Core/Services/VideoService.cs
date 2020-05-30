using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class VideoService: IVideoService
    {
        private CrowdFundingDbContext context_;
        public VideoService(CrowdFundingDbContext context)
        {
            context_ = context;
        }

        public Result<Video> CreateVideo(int projectId,
            CreateVideoOptions options)
        {
            if (options == null)
            {
                return Result<Video>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Url))
            {
                return Result<Video>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty Url");
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();

            if (project == null)
            {
                return Result<Video>.ActionFailed(
                    StatusCode.BadRequest, "Invalid projectId");
            }

            var video = new Video()
            {
                Url = options.Url,               
            };

            project.Videos.Add(video);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Video>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Video>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Video could not be added");
            }

            return Result<Video>.ActionSuccessful(video);
        }

        public bool DeleteVideo(int? videoId)
        {
            if (videoId == null)
            {
                return false;
            }

            var video = SearchVideo(new SearchVideoOptions()
            {
                VideoId = videoId
            }).SingleOrDefault();

            if (video == null)
            {
                return false;
            }

            context_.Remove(video);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public IQueryable<Video> SearchVideo(
            SearchVideoOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<Video>()
                .AsQueryable();            

            if (options.VideoId != null)
            {
                query = query.Where(v => v.VideoId == options.VideoId.Value);
            }

            query = query.Take(500);

            return query;
        }
    }
}
