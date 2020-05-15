using System.Collections.Generic;

namespace crowdFunding.Options
{
    public class SearchRewardPackageOptions
    {
        public List<Reward> Reward { get; set; }
        public int? RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public int? ProjectId { get; set; }
        public string Description { get; set; }
    }
}
