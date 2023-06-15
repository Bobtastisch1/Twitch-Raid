using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchRaid.Models
{
    internal class LiveStreamList
    {
        public List<Streams> streamers { get; set; }
    }

    internal class LiveStreamsDTO
    {
        public List<Streams> data { get; set; }
        public PaginationStreams pagination { get; set; }
    }

    internal class PaginationStreams
    {
        public string cursor { get; set; }
    }

    internal class Streams
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string user_login { get; set; }
        public string user_name { get; set; }
        public string game_id { get; set; }
        public string game_name { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int viewer_count { get; set; }
        public DateTime started_at { get; set; }
        public string language { get; set; }
        public string thumbnail_url { get; set; }
        public List<string> tag_ids { get; set; }
        public List<string> tags { get; set; }
        public bool is_mature { get; set; }
    }
}
