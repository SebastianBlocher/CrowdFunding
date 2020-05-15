using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class CreateUserOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public Project NewProject { get; set; }
        public int? NewBackedProjectId { get; set; }
        public decimal? BackedProjectAmount { get; set; }
    }
}
