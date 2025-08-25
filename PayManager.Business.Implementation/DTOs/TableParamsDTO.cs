using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.Business.Implementation.DTOs
{
    public class SortParams
    {
        [JsonProperty("selector")]
        public string Selector { get; set; }

        [JsonProperty("desc")]
        public bool Desc { get; set; }
    }


    public class TableParamsDTO
    {
        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("take")]
        public int Take { get; set; } = 20;

        [JsonProperty("filter")]
        public string? Filter { get; set; }

        [JsonProperty("sort")]
        public string? Sort { get; set; }

        [JsonProperty("searchValue")]
        public string? SearchValue { get; set; }
    }
}
