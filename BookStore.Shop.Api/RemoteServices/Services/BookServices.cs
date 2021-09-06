using BookStore.Shop.Api.RemoteModels;
using BookStore.Shop.Api.RemoteServices.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.RemoteServices.Services
{
    public class BookServices : IBookServices
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;

        public BookServices(IHttpClientFactory httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, BookRM book, string error)> GetBook(Guid BookId)
        {
            try
            {
                var httpClient = _httpClient.CreateClient("Books");
                var response = await httpClient.GetAsync($"api/Book/{BookId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var opt = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var result = JsonSerializer.Deserialize<BookRM>(content);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
