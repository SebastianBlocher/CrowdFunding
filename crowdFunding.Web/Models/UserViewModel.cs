using crowdFunding.Core.Model;
using System.Collections.Generic;

namespace crowdFunding.Web.Models
{
    public class UserViewModel
    {
        public User User { get; set; }        
        public List<Project> CreatedProjectsList { get; set; }
        public List<BackedProjects> BackedProjectsList { get; set; }

        public UserViewModel()
        {
            CreatedProjectsList = new List<Project>();
            BackedProjectsList = new List<BackedProjects>();            
        }

    }
}
