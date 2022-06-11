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
    public class FacturacionController : ControllerBase
    {
        private IUserServices userService;
        private readonly ILogger<FacturacionController> logger;
        private ApplicationDbContext context;
        private ValidarFacturas vlFac;

        public FacturacionController(ILogger<FacturacionController> _logger, ApplicationDbContext _Context, IUserServices _userService){
            logger = _logger;
            context = _Context;
            userService = _userService;
            vlFac = new ValidarFacturas(context);
        }

        


        // GET: api/<EmitirFac>
        [HttpGet("ConsultarFacturas")]
        public IEnumerable<FC_FACTURA_ENCABEZADO> Get()
        {
            
            var data = context.FC_FACTURA_ENCABEZADO.ToList();
            return data;
        }

        // GET api/<EmitirFac>/5
        
        [HttpGet("ConsultarFacturas/{token}")]
        public FC_FACTURA_ENCABEZADO Get(string token)
        {
            var data = context.FC_FACTURA_ENCABEZADO.Where(wr => wr.TOKEN.Equals(token)).FirstOrDefault();
            return data;
        }

        // POST api/<EmitirFac>
        [HttpPost("EmitirFactura")]
        public string Post(ModelNewFac model)
        {
            //var model = JsonConvert.DeserializeObject<ModelNewFac>(value);
            var isValid = vlFac.validarFacturaNueva(model);

            if(!isValid.estado){
                return isValid.mensaje;
            }

            var certificar = vlFac.guardarFacturaAsync(model).Result;
            if(!certificar.estado){
                certificar.mensaje = "Error al ingresar la factura => " + certificar.mensaje;
            }
            return certificar.mensaje;
        }

    
        // DELETE api/<EmitirFac>/5
        [HttpDelete("AnularFactura/{token}")]
        public string Delete(string token)
        {
            var anulacin = vlFac.AnularFacturaAsync(token).Result;
            return anulacin.mensaje;
        }
        
    }
}