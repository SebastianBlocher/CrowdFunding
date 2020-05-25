using crowdFunding.Core.Services.Options.Create;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateRewardPackageOptions
    {               
        public decimal? Amount { get; set; }        
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
