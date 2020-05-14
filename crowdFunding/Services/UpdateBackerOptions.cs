using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services
{
    public class UpdateBackerOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BackerId { get; set; }
        public string Country { get; set; }
        public decimal Amount { get; set; }
        public bool? IsActive { get; set; }
    }
}
