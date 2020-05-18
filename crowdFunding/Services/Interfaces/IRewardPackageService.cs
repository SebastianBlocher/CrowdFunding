using crowdFunding.Options;
using System.Linq;

namespace crowdFunding.Services
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
