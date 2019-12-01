using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectASP.Helpers;
using ProjectASP.Models;

namespace ProjectASP.Services
{
    public class UserService: IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly MemberContext _memberContext;
        public UserService(IOptions<AppSettings> appSettings, MemberContext memberContext)
        {
            _appSettings = appSettings.Value;
            _memberContext = memberContext;
        }
        
       

        public Gebruiker Authenticate(string username, string password)
        {
            var gebruiker = _memberContext.Gebruikers.SingleOrDefault(x => x.Gebruikersnaam == username && x.Wachtwoord == password);
            // return null if user not found
            if (gebruiker == null)
                return null;
            // authentication successful so generate jwttoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("GebruikerID", gebruiker.GebruikerID.ToString()),
                    new Claim("Email", gebruiker.Email),
                    new Claim("Gebruikersnaam", gebruiker.Gebruikersnaam)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            gebruiker.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            gebruiker.Wachtwoord = null;
            return gebruiker;
        }

        }
    }
