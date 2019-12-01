using ProjectASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP.Services
{
    public interface IUserService
    {
        Gebruiker Authenticate(string username, string password);
        

    }
}
