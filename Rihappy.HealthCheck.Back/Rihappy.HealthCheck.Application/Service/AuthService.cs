using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihappy.HealthCheck.Application.Service
{
    public class AuthService
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "1234";

        public bool ValidateCredentials(string username, string password)
        {
            return username == ValidUsername && password == ValidPassword;
        }
    }
}
