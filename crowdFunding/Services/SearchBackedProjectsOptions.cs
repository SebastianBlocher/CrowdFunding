using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services
{
    public class SearchBackedProjectsOptions
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        //public int? UserId { get; set; }
    }
}
