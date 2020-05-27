using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateRewardPackageOptions
    {           
        public decimal? Amount { get; set; }
        public string Description { get; set; }       
        public string Name { get; set; }
        public List<CreateRewardOptions> RewardOptions { get; set; }        

        public CreateRewardPackageOptions()
        {
            RewardOptions = new List<CreateRewardOptions>();
        }
    }
}
