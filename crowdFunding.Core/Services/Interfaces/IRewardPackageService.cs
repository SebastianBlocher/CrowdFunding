using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IRewardPackageService
    {
        RewardPackage CreateRewardPackage(CreateRewardPackageOptions options);
        IQueryable<RewardPackage> SearchRewardPackage(SearchRewardPackageOptions options);
        RewardPackage UpdateRewardPackage(UpdateRewardPackageOptions options);
        RewardPackage GetRewardPackageById(int? rewardPackageId);
        bool RemoveRewardPackage(int? rewardPackageId);
    }
}
