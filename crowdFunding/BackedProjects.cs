﻿using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class BackedProjects
    {
        public int BackedProjectsId { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset BackedOn { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }

        public BackedProjects()
        {
            BackedOn = DateTimeOffset.Now;
        }
    }
}
