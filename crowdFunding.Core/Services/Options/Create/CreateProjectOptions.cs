using crowdFunding.Core.Model;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateProjectOptions
    {       
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }        
        public decimal? AmountRequired { get; set; }
      
    }
}

