using System;
using System.Collections.Generic;
using System.Text;
using TenForce_consolApp.Models;

namespace TenForce_consolApp.Output
{
    public class ConsoleOutputService
    {
        public void DisplayPlanetsWithAverageTemperature(List<Planet> planets)
        {
            Console.WriteLine("\n===== PLANETS WITH AVERAGE MOON TEMPERATURE =====");

            if (planets == null || planets.Count == 0)
            {
                Console.WriteLine("No planets with moons found.");
                return;
            }

            foreach (var planet in planets)
            {
                Console.WriteLine($"\nPlanet: {planet.Name}");
                Console.WriteLine($"Number of Moons: {planet.Moons.Count}");

                double? averageTemperature =
                    planet.GetAverageMoonTemperature();

                if (averageTemperature.HasValue)
                {
                    Console.WriteLine(
                        $"Average Moon Temperature: {averageTemperature.Value:F2} K");
                }
                else
                {
                    Console.WriteLine(
                        "Average Moon Temperature: Not available");
                }
            }
        }
    }
}
