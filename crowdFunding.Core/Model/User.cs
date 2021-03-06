﻿using System;
using System.Collections.Generic;

namespace crowdFunding.Core.Model
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
        public bool IsActive { get; set; }
        
        public User()
        {
            CreatedOn = DateTimeOffset.Now;
            IsActive = true;
            CreatedProjectsList = new List<Project>();
            BackedProjectsList = new List<BackedProjects>();
        }
    }
}
