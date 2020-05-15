namespace crowdFunding.Options
{
    public class SearchRewardPackageOptions
    {        
        public int? RewardPackageId { get; set; }
        public decimal? Amount { get; set; }
        public int? ProjectId { get; set; }
        public string Description { get; set; }
    }
}
