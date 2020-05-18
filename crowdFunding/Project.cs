using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class Project
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string Description { get; set; }
        public List<RewardPackage> Rewards { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public Project()
        {
            CreatedOn = DateTimeOffset.Now;
            Rewards = new List<RewardPackage>();
        }
    }
}
