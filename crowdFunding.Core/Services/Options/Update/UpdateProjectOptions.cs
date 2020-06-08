using crowdFunding.Core.Model;
using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateProjectOptions
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public Category? Category { get; set; }        
        public decimal? AmountRequired { get; set; }
        public DateTime DueTo { get; set; }
        public List<string> Photos { get; set; }
        public List<string> Videos { get; set; }

        public UpdateProjectOptions()
        {
            Photos = new List<string>();
            Videos = new List<string>();
            DueTo = new DateTime();
        }
    }
 }

