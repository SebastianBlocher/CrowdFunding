using System;

namespace crowdFunding.Core.Model
{
    public class Posts
    {
        public int PostsId { get; set; }
        public string Post{ get; set; }
        public DateTime CreatedOn { get; set; }

        public Posts()
        {
            CreatedOn = DateTime.Today;
        }
    }
}
