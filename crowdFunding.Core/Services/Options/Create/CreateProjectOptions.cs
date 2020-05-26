using crowdFunding.Core.Model;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateProjectOptions
    {
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
        public List<Reward> RewardPackages { get; set; }
        public decimal? AmountRequired { get; set; }

        public CreateProjectOptions()
        {
        }
    }
}

