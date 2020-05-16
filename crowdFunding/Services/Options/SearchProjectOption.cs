using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class SearchProjectOption
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
    }

}
