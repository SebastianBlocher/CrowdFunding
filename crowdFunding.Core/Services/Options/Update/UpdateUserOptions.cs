namespace crowdFunding.Core.Services.Options.Update
{
    public class UpdateUserOptions
    {        
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}
