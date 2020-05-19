using crowdFunding.Options;
using System.Linq;

namespace crowdFunding.Services
{
    public interface IRewardService
    {
        Reward CreateReward(CreateRewardOptions options);
        IQueryable<Reward> SearchReward(SearchRewardOptions options);
        Reward UpdateReward(UpdateRewardOptions options);
        Reward GetRewardById(int? rewardId);
        bool RemoveReward(int? rewardId);
    }
}
