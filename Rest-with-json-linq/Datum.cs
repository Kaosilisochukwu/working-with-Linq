using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Rest_with_json_linq
{
    public class Datum
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
        public int Submitted { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("submission_count")]
        public int SubmissionCount { get; set; }

        [JsonPropertyName("comment_count")]
        public int CommentCount { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
    }
}
