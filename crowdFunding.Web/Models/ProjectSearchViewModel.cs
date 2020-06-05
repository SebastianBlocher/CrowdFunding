using crowdFunding.Core.Model;
using System.Collections.Generic;

namespace crowdFunding.Web.Models
{
    public class ProjectSearchViewModel
    {
        public List<Project> ProjectList { get; set; }        

        public ProjectSearchViewModel()
        {
            ProjectList = new List<Project>();           
        }
    }
}
