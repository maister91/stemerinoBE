using ProjectASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectASP
{
    public class DBInitializer
    {
        public static void Initialize(MemberContext context)
        {
            context.Database.EnsureCreated();

            if (context.Gebruikers.Any())
            {
                return;
            }
            context.Gebruikers.AddRange(
                new Gebruiker {Email = "melih.91@hotmail.com", Wachtwoord= "melih", Gebruikersnaam= "melih" },
                 new Gebruiker { Email = "test@hotmail.com", Wachtwoord = "test", Gebruikersnaam = "test" });
            context.SaveChanges();
        }
    }
}
