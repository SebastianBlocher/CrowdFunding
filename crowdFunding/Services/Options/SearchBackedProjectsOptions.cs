using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services
{
    public class SearchBackedProjectsOptions
    {
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public DateTimeOffset? BackedFrom { get; set; }
        public DateTimeOffset? BackedTo { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
    }
}
