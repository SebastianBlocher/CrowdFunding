using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Model
{
    public class Project
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string Description { get; set; }
        public List<RewardPackage> RewardPackages { get; set; }
        public Category Category { get; set; }
        public decimal AmountRequired { get; set; }
        public decimal AmountGathered { get; set; }
        public int NumberOfBackers { get; set; }
        public bool IsActive { get; set; }

        

        public Project()
        {
            CreatedOn = DateTimeOffset.Now;
            RewardPackages = new List<RewardPackage>();
            NumberOfBackers = 0;
            AmountGathered = 0;
            IsActive = true;
            //Category.ToString();
            //var categoryArray = Enum.GetValues(typeof(Category));
        }
    }
}
