using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class CreateProjectOption
    {
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Category? Category { get; set; }
        public List<Reward> Rewards  { get; set; } 

        public CreateProjectOption()
        {
        }
    }
}

