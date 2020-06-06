using System;

namespace crowdFunding.Core.Model
{
    public class BackedProjects
    {
        public int BackedProjectsId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }                
        public string Description { get; set; }        
        public Category Category { get; set; }  
        public int NumberOfBackers { get; set; }
        public string Photo { get; set; }        
        public int ProjectCreatorId { get; set; }
        public string ProjectCreatorFirstName { get; set; }
        public string ProjectCreatorLastName { get; set; }
        public decimal Amount { get; set; }
    }
}
    //public 
    //    public int ProjectId { get; set; }
    //    public int UserId { get; set; }
    //    public DateTimeOffset BackedOn { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public Category Category { get; set; }

    //    public BackedProjects()
    //    {

    //        BackedOn = DateTimeOffset.Now;
    //    }
 
