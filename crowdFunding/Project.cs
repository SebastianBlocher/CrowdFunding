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
        public List<ProjectBacker> ProjectBackers { get; set; }
        public List<RewardPackage> Rewards { get; set; }
        public Category Category { get; set; }
        public Project()
        {
            CreatedOn = DateTimeOffset.Now;
            ProjectBackers = new List<ProjectBacker>();
            Rewards = new List<RewardPackage>();
        }
    }
}
