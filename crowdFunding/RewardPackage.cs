using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class RewardPackage
    {
        public List<Reward> Reward { get; set; }
        public int RewardPackageId { get; set; }
        public decimal Ammount { get; set; }

        public RewardPackage()
        {
            Reward = new List<Reward>();
        }
    }
}
