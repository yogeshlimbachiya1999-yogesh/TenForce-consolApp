using System;
using System.Collections.Generic;
using System.Text;

namespace TenForce_consolApp.Models
{
    public class Planet
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<Moon> Moons { get; set; } = new();

        public double? GetAverageMoonTemperature()
        {
            var temperatures = Moons
                .Where(m => m.AverageTemperature.HasValue &&
                            m.AverageTemperature.Value > 0)
                .Select(m => m.AverageTemperature!.Value)
                .ToList();

            if (temperatures.Count == 0)
            {
                return null;
            }

            return temperatures.Average();
        }
    }
}
