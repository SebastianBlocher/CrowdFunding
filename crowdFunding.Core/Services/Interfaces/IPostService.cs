using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IPostService
    {
        Result<Posts> CreatePost(int projectId, CreatePostOptions options);
        bool DeletePost(int? postId);
        IQueryable<Posts> SearchPost(SearchPostOptions options);
        Result<Posts> UpdatePost(int postId, UpdatePostOptions options);
    }
}
