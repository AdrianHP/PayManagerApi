using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayManager.ApiService.Models
{
    public class OrderResponse
    {
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("fees")]
        public List<Fee> Fees { get; set; }

        [JsonProperty("products")]
        public List<ProductModel> Products { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }
    } 
}
