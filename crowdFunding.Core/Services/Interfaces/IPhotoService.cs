using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IPhotoService
    {
        Result<Photo> CreatePhoto(int projectId, CreatePhotoOptions options);
        Result<bool> DeletePhoto(int? photoId);
        IQueryable<Photo> SearchPhoto(SearchPhotoOptions options);
    }
}
