namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateBackedProjectOptions
    {
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
