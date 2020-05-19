﻿using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Services.Options
{
    public class UpdateUserOptions
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
