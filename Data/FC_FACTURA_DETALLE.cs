using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_FACTURA_DETALLE", Schema = "dbo")]
    
    public class FC_FACTURA_DETALLE
    {
        [Key]
        public int ID_DETALLE { get; set; }
        public int ID_FAC_ENCABEZADO { get; set; }
        public int NUM_LINEA { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal IVA { get; set; }
        public decimal PRECIO_VENTA { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal DESCUENTO_UNIDAD { get; set; }
        public decimal DESCUENTO_TOTAL { get; set; }
        public decimal TOTAL_LINEA { get; set; }
    }
}