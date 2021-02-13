using System.Net.Http;
using Hahn.ApplicatonProcess.December2020.Domain.Repositories.Interfaces;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private HttpClient _httpClient;

        public CountryRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Countries.API");
        }
        public bool SearchCountryValid(string countryName)
        {
            string url = $"rest/v2/name/{countryName}?fullText=true";
            var response = _httpClient.GetAsync(_httpClient.BaseAddress + url).Result;
            return response.IsSuccessStatusCode;
        }
    }
}