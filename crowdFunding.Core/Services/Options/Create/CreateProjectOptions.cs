using crowdFunding.Core.Model;
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
        public List<Photo> Photos { get; set; }
        public List<Video> Videos { get; set; }

        public CreateProjectOptions()
        {
            Photos = new List<Photo>();
            Videos = new List<Video>();
        }
    }   
}

