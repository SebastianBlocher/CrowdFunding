using crowdFunding.Core.Model;

namespace crowdFunding.Core.Services.Options.Search
{
    public class SearchProjectOptions
    {
        public int? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
    }
}
