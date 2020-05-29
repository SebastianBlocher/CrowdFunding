using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
{
    public interface IRewardService
    {
        Result<Reward> CreateReward(int rewardPackageId, CreateRewardOptions options);
        Result<Reward> AddRewardToList(CreateRewardOptions options);
        IQueryable<Reward> SearchReward(SearchRewardOptions options);
        Result<Reward> UpdateReward(int rewardId, UpdateRewardOptions options);
        Reward GetRewardById(int? rewardId);
        bool RemoveReward(int? rewardId);
    }
}
