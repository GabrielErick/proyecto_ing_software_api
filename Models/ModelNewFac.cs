using System.Security.Cryptography.X509Certificates;
namespace proyecto_ing_software_api.Models
{
    public class ModelNewFac
    {
        public int idSerie { get; set; }
        public int idCliente { get; set; }
        public int idTiendaCliente { get; set;}
        public string nombreClienteFinal { get; set; }
        public string direccionClienteFinal { get; set; }
        public string nitClienteFinal { get; set; }
        public decimal totalFactura { get; set; }
        public string correoClienteFinal { get; set; }
        public string numFacturaLocal { get; set; }
        public modelDetalleNewFac[] detalleFactura { get; set; }

    }

    public class modelDetalleNewFac
    {
        public int numLinea { get; set; }
        public string descripcion { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal iva { get; set; }
        public decimal precioVenta { get; set; }
        public decimal cantidad { get; set; }
        public decimal descuentoUnitario { get; set; }
        public decimal descuentoTotal { get; set; }
        public decimal totalLinea { get; set; }
    }
}