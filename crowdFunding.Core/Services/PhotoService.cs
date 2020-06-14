using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class PhotoService: IPhotoService
    {
        private CrowdFundingDbContext context_;
        public PhotoService(CrowdFundingDbContext context)
        {
            context_ = context;
        }

        public Result<Photo> CreatePhoto(int projectId,
            CreatePhotoOptions options)
        {
            if (options == null)
            {
                return Result<Photo>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Url))
            {
                return Result<Photo>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty Url");
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();

            if (project == null)
            {
                return Result<Photo>.ActionFailed(
                    StatusCode.BadRequest, "Invalid ProjectId");
            }

            var photo = new Photo()
            {
                Url = options.Url               
            };

            project.Photos.Add(photo);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Photo>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Photo>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Photo could not be added");
            }

            return Result<Photo>.ActionSuccessful(photo);
        }

        public bool DeletePhoto(int? photoId)
        {
            if (photoId == null)
            {
                return false;
            }

            var photo = SearchPhoto(new SearchPhotoOptions()
            {
                PhotoId = photoId
            }).SingleOrDefault();

            if (photo == null)
            {
                return false;
            }

            context_.Remove(photo);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public IQueryable<Photo> SearchPhoto(
            SearchPhotoOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<Photo>()
                .AsQueryable();

            if (options.PhotoId != null)
            {
                query = query.Where(p => p.PhotoId == options.PhotoId.Value);
            }

            query = query.Take(500);

            return query;
        }
    }
}
