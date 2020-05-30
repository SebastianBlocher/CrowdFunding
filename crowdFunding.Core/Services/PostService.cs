using crowdFunding.Core.Data;
using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Interfaces;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System;
using System.Linq;

namespace crowdFunding.Core.Services
{
    public class PostService: IPostService
    {
        private CrowdFundingDbContext context_;
        public PostService(CrowdFundingDbContext context)
        {
            context_ = context;
        }

        public Result<Posts> CreatePost(int projectId,
            CreatePostOptions options)
        {
            if (options == null)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Post))
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.BadRequest, "Null or empty Post");
            }

            var project = context_
                .Set<Project>()
                .Where(p => p.ProjectId == projectId)
                .SingleOrDefault();

            if (project == null)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.BadRequest, "Invalid projectId");
            }

            var post = new Posts()
            {
                Post = options.Post                
            };

            project.Posts.Add(post);

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Post could not be created");
            }

            return Result<Posts>.ActionSuccessful(post);
        }

        public bool DeletePost(int? postId)
        {
            if (postId == null)
            {
                return false;
            }

            var post = SearchPost(new SearchPostOptions()
            {
                PostId = postId
            }).SingleOrDefault();

            if (post == null)
            {
                return false;
            }

            context_.Remove(post);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public IQueryable<Posts> SearchPost(
            SearchPostOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                .Set<Posts>()
                .AsQueryable();

            if (options.PostId != null)
            {
                query = query.Where(p => p.PostsId == options.PostId.Value);
            }

            query = query.Take(500);

            return query;
        }

        public Result<Posts> UpdatePost(
        int postId,
        UpdatePostOptions options)
        {
            var result = new Result<Posts>();

            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";

                return result;
            }

            var post = SearchPost(new SearchPostOptions()
            {
                PostId = postId
            }).SingleOrDefault();

            if (post == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Post with id {postId} was not found";

                return result;
            }

            if (!string.IsNullOrWhiteSpace(options.Post))
            {
                post.Post = options.Post;
            }

            var rows = 0;

            try
            {
                rows = context_.SaveChanges();
            }
            catch (Exception ex)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.InternalServerError, ex.ToString());
            }

            if (rows <= 0)
            {
                return Result<Posts>.ActionFailed(
                    StatusCode.InternalServerError,
                    "Post could not be updated");
            }

            return Result<Posts>.ActionSuccessful(post);
        }
    }   
}
