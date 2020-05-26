namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateRewardPackageOptions
    {   
        public int RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }       
        public string Name { get; set; }
    }
}
