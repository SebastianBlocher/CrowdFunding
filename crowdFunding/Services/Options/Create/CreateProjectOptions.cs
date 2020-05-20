using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class CreateProjectOptions
    {
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
        public List<Reward> RewardPackages { get; set; }
        public decimal? AmountRequiered { get; set; }

        public CreateProjectOptions()
        {
        }
    }
}

