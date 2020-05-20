using System;
using System.Collections.Generic;
using System.Text;

namespace crowdFunding.Core.Services.Options.Search
{
    public class SearchUserOptions
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime? CreateOnFrom { get; set; }
        public DateTime? CreateOnTo { get; set; }
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get; set; }
    }
}
