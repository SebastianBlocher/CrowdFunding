using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding
{
    public class ProjectBacker
    {
        public int ProjectId { get; set; }
        public int BackerId { get; set; }
        public Project Project { get; set; }
        public Backer Backer { get; set; }
    }
}
