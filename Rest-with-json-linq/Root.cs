using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Rest_with_json_linq
{
    public class Root
    {
        public string Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        public List<Datum> Data { get; set; }
    }
}
