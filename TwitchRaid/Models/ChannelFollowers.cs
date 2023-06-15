using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchRaid.Models
{
        internal class FollowerList
        {
            public List<Follower> follower { get; set; }
        }
        internal class FollowersDTO
        {
            public List<Follower> data { get; set; }
            public Pagination pagination { get; set; }
            public int total { get; set; }
        }

        internal class Follower
        {
            public string followed_at { get; set; }
            public string user_id { get; set; }
            public string user_login { get; set; }
            public string user_name { get; set; }
        }

        internal class Pagination
        {
            public string cursor { get; set; }
        }
    
}
