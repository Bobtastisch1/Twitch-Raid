using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchRaid.Models
{

    internal class RaidDTO
    {
        public List<Raid> data { get; set; }
    }

    internal class Raid
    {
            public DateTime created_at { get; set; }
            public bool is_mature { get; set; }
    }
}
