﻿using System.Collections.Generic;

namespace crowdFunding.Core.Model
{
    public class RewardPackage
    {
        public List<Reward> Rewards { get; set; }
        public string Name { get; set; }
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public RewardPackage()
        {
            Rewards = new List<Reward>();
        }
    }
}
