using System.Collections.Generic;

namespace crowdFunding.Options
{
    public class UpdateRewardPackageOptions
    {
        public List<CreateRewardOptions> Rewards { get; set; }
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }        
        public string Description { get; set; }
        public string Name { get; set; }

        public UpdateRewardPackageOptions()
        {
            Rewards = new List<CreateRewardOptions>();
        }
    }
}
