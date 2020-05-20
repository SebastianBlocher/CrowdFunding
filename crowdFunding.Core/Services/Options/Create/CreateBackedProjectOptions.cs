using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateBackedProjectOptions
    {
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
