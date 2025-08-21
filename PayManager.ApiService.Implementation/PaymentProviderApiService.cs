using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

//using Newtonsoft.Json;
using PayManager.ApiService.Models;
using System.Net.Http.Json;
using System.Text.Json;


namespace PayManager.ApiService.Implementation
{
    public class PaymentProviderApiService : IPaymentProviderApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentProviderApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<OrderResponse> CreateOrder(OrderCreateModel order,string provider)
        {

            var apiUrl = _configuration["PaymentProviders:" + provider + ":BaseUrl"];
            var apiKey = _configuration["PaymentProviders:" + provider + ":ApiKey"];
            _httpClient.BaseAddress = new Uri(apiUrl);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            var response = await _httpClient.PostAsJsonAsync($"order", order);
            var jsonString = await response.Content.ReadAsStringAsync();
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(jsonString);
            return orderResponse;
        }

        public Task<IEnumerable<OrderResponse>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
