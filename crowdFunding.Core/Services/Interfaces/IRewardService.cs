using crowdFunding.Core.Model;
using crowdFunding.Core.Services.Options.Create;
using crowdFunding.Core.Services.Options.Search;
using crowdFunding.Core.Services.Options.Update;
using System.Linq;

namespace crowdFunding.Core.Services.Interfaces
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
