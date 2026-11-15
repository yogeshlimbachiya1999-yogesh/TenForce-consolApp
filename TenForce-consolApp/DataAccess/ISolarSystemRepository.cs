using System;
using System.Collections.Generic;
using System.Text;
using TenForce_consolApp.Models;

namespace TenForce_consolApp.DataAccess
{
    public interface ISolarSystemRepository
    {
        Task<List<ApiBody>> GetAllBodiesAsync(
       CancellationToken cancellationToken = default);
    }
}
