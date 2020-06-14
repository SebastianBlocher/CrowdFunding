using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Model
{
    public class Project
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DueTo { get; set; }
        public string Description { get; set; }
        public List<RewardPackage> RewardPackages { get; set; }
        public Category Category { get; set; }
        public decimal AmountRequired { get; set; }
        public decimal AmountGathered { get; set; }
        public int NumberOfBackers { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Video> Videos { get; set; }
        public List<Posts> Posts { get; set; } 
        public User User { get; set; }
        public int UserId { get; set; }
        public Project()
        {
            CreatedOn = DateTime.Today;
            RewardPackages = new List<RewardPackage>();
            NumberOfBackers = 0;
            AmountGathered = 0;
            DueTo = new DateTime();
            Posts = new List<Posts>();
            Photos = new List<Photo>();
            Videos = new List<Video>();
        }

        public string Percentage()
        {
            var percentage = (AmountGathered / AmountRequired) * 100;
            return percentage.ToString("0.00")+"%";
   
        }
    }
}
