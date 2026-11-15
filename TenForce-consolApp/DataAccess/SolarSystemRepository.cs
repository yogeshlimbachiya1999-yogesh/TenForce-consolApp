using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TenForce_consolApp.Models;

namespace TenForce_consolApp.DataAccess
{
    public class SolarSystemRepository : ISolarSystemRepository
    {
        private readonly HttpClient _httpClient;

        private const string ApiUrl =
            "https://api.le-systeme-solaire.net/rest/bodies/";

        public SolarSystemRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;

            string? apiKey =
                Environment.GetEnvironmentVariable("SOLAR_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "SOLAR_API_KEY environment variable is missing.");
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<List<ApiBody>> GetAllBodiesAsync(
            CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Loading Solar System data...");

            using var response = await _httpClient.GetAsync(
                ApiUrl, cancellationToken);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync(
                cancellationToken);

            var result = JsonSerializer.Deserialize<ApiBodiesResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result?.Bodies == null)
            {
                throw new InvalidOperationException(
                    "Unable to read Solar System API response.");
            }

            Console.WriteLine(
                $"Loaded {result.Bodies.Count} Solar System bodies.");

            return result.Bodies;
        }
    }
}
