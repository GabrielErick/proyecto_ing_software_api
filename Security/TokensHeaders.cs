using Microsoft.AspNetCore.Mvc;

namespace proyecto_ing_software_api.Security
{
    public class TokensHeaders
    {
        [FromHeader]
        public string FirstName { get; set; }
    }
}