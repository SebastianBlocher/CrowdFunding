using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class BackerRewardPackage
    {
        public int BackerId { get; set; }
        public int RewardPackageId { get; set; }
        public Backer Backer { get; set; }
        public RewardPackage RewardPackage { get; set; }
    }
}
