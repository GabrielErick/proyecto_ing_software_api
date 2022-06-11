using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proyecto_ing_software_api.Data;
using proyecto_ing_software_api.Libraries;
using proyecto_ing_software_api.Models;
using proyecto_ing_software_api.Security;

namespace proyecto_ing_software_api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AdministracionController : ControllerBase
    {
        public ApplicationDbContext context;
        private readonly ILogger<AdministracionController> logger;

        public AdministracionController(ILogger<AdministracionController> _logger, ApplicationDbContext _context){
            logger = _logger;
            context = _context;
        }

        

         
    }
}