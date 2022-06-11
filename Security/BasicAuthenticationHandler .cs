using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using proyecto_ing_software_api.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace proyecto_ing_software_api.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        
        private readonly IUserServices userService;
        private ApplicationDbContext context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock clock, IUserServices _userService, ApplicationDbContext _context) 
            : base(options, logger, encoder, clock)
        {
            userService = _userService;
            context = _context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            var endpoint = Context.GetEndpoint();
            
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null){
                var response = AuthenticateResult.NoResult();
                return response;
            }
                
            if (!Request.Headers.ContainsKey("Authorization")){
                return AuthenticateResult.Fail("Missing Authorization Header");
            }
                
            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                //user = await userService.Authenticate(username, password);
                user = await (from us in context.FC_USUARIOS
                        where us.USUARIO.Equals(username) && us.PASSWORD.Equals(password)
                        select new User{
                            Id = us.ID_USUARIO,
                            Username = us.USUARIO,
                            Password = us.PASSWORD
                        }).FirstOrDefaultAsync();
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            if (user == null){
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
                
            var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                    };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            var responsetwo = AuthenticateResult.Success(ticket);
            return responsetwo;



        }
    }
}