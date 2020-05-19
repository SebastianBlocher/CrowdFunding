using crowdFunding.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class UpdateProjectOption
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; } // isos na min mas xreiazetai, giati na allaksei katigoria px.
        public decimal? Amount { get; set; }
        public RewardPackage Rewards { get; set; }

        public UpdateProjectOption()
            {
             Rewards = new CreateRewardOptions();
             UpdatedOn = DateTimeOffset.Now;

            }

    }
}
