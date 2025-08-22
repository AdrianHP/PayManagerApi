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

            SetHttpClient(provider);
            if (provider == "CazaPagos" && order.Method == "Card")
                order.Method = "CreditCard";
            var response = await _httpClient.PostAsJsonAsync($"order", order);
            var jsonString = await response.Content.ReadAsStringAsync();
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(jsonString);
            return orderResponse;
        }

        public async Task PayOrder(string orderId, string provider)
        {
            SetHttpClient(provider);
            var response = await _httpClient.PutAsJsonAsync($"pay?Id="+ orderId, orderId);
        }
        public async Task CancelOrder(string orderId, string provider)
        {
            SetHttpClient(provider);
            var response = await _httpClient.PutAsJsonAsync($"cancel?Id=" + orderId, orderId);
        }

        private void SetHttpClient(string providerName)
        {
            var apiUrl = _configuration["PaymentProviders:" + providerName + ":BaseUrl"];
            var apiKey = _configuration["PaymentProviders:" + providerName + ":ApiKey"];
            _httpClient.BaseAddress = new Uri(apiUrl);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public Task<IEnumerable<OrderResponse>> GetOrders()
        {
            throw new NotImplementedException();
        }

       
    }
}
