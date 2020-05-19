using System.Collections.Generic;

namespace crowdFunding.Options
{
    public class CreateRewardPackageOptions
    {
        public List<CreateRewardOptions> RewardOptions { get; set; }        
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public CreateRewardPackageOptions()
        {
            RewardOptions = new List<CreateRewardOptions>();            
        }
    }
}
