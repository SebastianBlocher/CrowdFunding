using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IVideoService
    {
        Result<Video> CreateVideo(int projectId, CreateVideoOptions options);
        bool DeleteVideo(int? videoId);
        IQueryable<Video> SearchVideo(SearchVideoOptions options);
    }
}
