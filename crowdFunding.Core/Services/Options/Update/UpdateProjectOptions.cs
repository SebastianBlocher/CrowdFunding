using crowdFunding.Core.Model;
using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateProjectOptions
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; } // isos na min mas xreiazetai, giati na allaksei katigoria px.
        public List<RewardPackage> RewardPackages { get; set; }
        public decimal? AmountRequired { get; set; }

        public UpdateProjectOptions()
        {
            RewardPackages = new List<RewardPackage>();
            UpdatedOn = DateTimeOffset.Now;
        }

    }
}
