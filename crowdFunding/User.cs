using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public List<Project> CreatedProjectsList { get; set; }
        public List<BackedProjects> BackedProjectsList { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }

        
        public User()
        {
            CreatedOn = DateTimeOffset.Now;
            CreatedProjectsList = new List<Project>();
            BackedProjectsList = new List<BackedProjects>();
        }
    }
}
