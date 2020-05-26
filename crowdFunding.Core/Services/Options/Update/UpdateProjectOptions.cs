using crowdFunding.Core.Model;
using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateProjectOptions
    {        
        public string Name { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
        //public List<RewardPackage> RewardPackages { get; set; }
        public decimal? AmountRequired { get; set; }

        public UpdateProjectOptions()
        {
            //RewardPackages = new List<RewardPackage>();
            UpdatedOn = DateTimeOffset.Now;
        }

    }
}
