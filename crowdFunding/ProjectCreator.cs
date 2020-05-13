using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class ProjectCreator
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProjectCreatorId { get; set; }
        public string Country { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public List<Project> ProjectList { get; set; }
        public string Description { get; set; }
        public ProjectCreator()
        {
            CreatedOn = DateTimeOffset.Now;
            ProjectList = new List<Project>();
        }
    }
}
