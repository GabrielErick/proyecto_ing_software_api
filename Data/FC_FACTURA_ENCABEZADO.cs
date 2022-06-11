using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_FACTURA_ENCABEZADO", Schema = "dbo")]
    public class FC_FACTURA_ENCABEZADO
    {
        [Key]
        public int ID_FAC_ENCABEZADO { get; set; }
        public int ID_SERIE { get; set; }
        public int ID_TIP_TOKEN { get; set; }
        public string TOKEN { get; set; }
        public int ID_CLIENTE { get; set; }
        public int ID_TIENDA_CLIENTE { get; set; }
        public string NOMBRE_CONTRIBUYENTE { get; set; }
        public string DIRECCION_CONTRIBUYENTE { get; set; }
        public string NIT_CONTRIBUYENTE { get; set; }
        public decimal TOTAL_FACTURA { get; set; }
        public string CORREO_CONTRIBUYENTE { get; set; }
        public string CORRELATIVO_CLIENTE { get; set; }
        public int ID_ESTADO_FACTURA { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime? FEC_BAJA { get; set; }
        public string IND_ESTADO { get; set; }
    }
}