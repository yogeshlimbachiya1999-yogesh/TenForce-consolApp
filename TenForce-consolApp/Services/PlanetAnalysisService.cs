using System;
using System.Collections.Generic;
using System.Text;
using TenForce_consolApp.Models;

namespace TenForce_consolApp.Services
{
    public class PlanetAnalysisService : IPlanetAnalysisService
    {
        public List<Planet> GetPlanetsWithMoonTemperatures(
            List<ApiBody> bodies)
        {
            Console.WriteLine(
                "Processing planets and their moons...");

            if (bodies == null || bodies.Count == 0)
            {
                return new List<Planet>();
            }

            var bodyLookup = bodies.ToDictionary(
                x => x.Id,
                StringComparer.OrdinalIgnoreCase);

            var planets = bodies
                .Where(x =>
                    x.IsPlanet &&
                    x.Moons != null &&
                    x.Moons.Count > 0)
                .Select(body =>
                {
                    var planet = new Planet
                    {
                        Id = body.Id,
                        Name = body.EnglishName
                    };

                    foreach (var moonReference in body.Moons!)
                    {
                        if (!bodyLookup.TryGetValue(
                            moonReference.MoonId,
                            out var moonBody))
                        {
                            continue;
                        }

                        if (!string.Equals(
                            moonBody.AroundPlanet?.PlanetId,
                            body.Id,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        planet.Moons.Add(new Moon
                        {
                            Id = moonBody.Id,
                            Name = moonBody.EnglishName,

                            AverageTemperature =
                                moonBody.AverageTemperature,

                            MassValue =
                                moonBody.Mass?.MassValue,

                            MassExponent =
                                moonBody.Mass?.MassExponent
                        });
                    }

                    return planet;
                })
                .Where(p => p.Moons.Count > 0)
                .ToList();

            Console.WriteLine(
                $"Found {planets.Count} planets with moons.");

            return planets;
        }
    }
}
