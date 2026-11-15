using System;
using System.Collections.Generic;
using System.Text;
using TenForce_consolApp.Models;

namespace TenForce_consolApp.Services
{
    public interface IPlanetAnalysisService
    {
        List<Planet> GetPlanetsWithMoonTemperatures(
       List<ApiBody> bodies);
    }
}
