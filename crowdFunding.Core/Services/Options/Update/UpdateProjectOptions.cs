using crowdFunding.Core.Model;
using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateProjectOptions
    {        
        public string Name { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }        
        public decimal? AmountRequired { get; set; }
        //public List<string> Photos { get; set; }
        //public List<string> Videos { get; set; }
        //public List<string> PostUpdates { get; set; }

        public UpdateProjectOptions()
        {
            UpdatedOn = DateTimeOffset.Now;
            //PostUpdates = new List<string>();
            //Photos = new List<string>();
            //Videos = new List<string>();
        }

    }
}
