using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class CreateProjectOption
    {
        public int ProjectId { get; set; }
        public List<string> UserIds { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Category Category { get; set; }
        public List<Reward> Rewards  { get; set; } // edo thelo na pairnei tis plirofories gia ta rewards poy yparxoun, to xasa.

        public CreateProjectOption()
        {
            UserIds = new List<string>();
        }
    }

}

