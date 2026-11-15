using System;
using System.Collections.Generic;
using System.Text;

namespace TenForce_consolApp.Models
{
    public class Moon
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public double? AverageTemperature { get; set; }

        public double? MassValue { get; set; }

        public int? MassExponent { get; set; }
    }
}
