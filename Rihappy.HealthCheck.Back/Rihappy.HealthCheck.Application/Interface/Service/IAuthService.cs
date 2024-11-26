using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.Interface.Service
{
    public interface IAuthService
    {
        bool ValidateCredentials(string username, string password);
        string GenerateJwtToken(string username);
    }
}
