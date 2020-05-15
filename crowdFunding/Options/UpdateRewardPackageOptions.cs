using System.Collections.Generic;

namespace crowdFunding.Options
{
    public class UpdateRewardPackageOptions
    {
        public List<Reward> Reward { get; set; }
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public List<int?> RewardIds { get; set; }
        public string Description { get; set; }

        public UpdateRewardPackageOptions()
        {
            RewardIds = new List<int?>();
        }
    }
}
