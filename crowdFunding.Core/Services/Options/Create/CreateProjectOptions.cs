using crowdFunding.Core.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateProjectOptions
    {       
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }        
        public decimal? AmountRequired { get; set; }
        public DateTime DueTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<string> Photos { get; set; }
        public List<string> Videos { get; set; }

        public CreateProjectOptions()
        {
            Photos = new List<string>();
            Videos = new List<string>();
            CreatedOn = DateTime.Today;
            DueTo = new DateTime();
        }
    }   
}

