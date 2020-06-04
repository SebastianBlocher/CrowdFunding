using crowdFunding.Core.Model;
using System;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateProjectOptions
    {        
        public string Name { get; set; }        
        public string Description { get; set; }
        public Category? Category { get; set; }        
        public decimal? AmountRequired { get; set; }
        public DateTime DueTo { get; set; }
    }
}
