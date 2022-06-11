using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_ESTADO_FACTURA", Schema = "dbo")]
    public class FC_ESTADO_FACTURA
    {
        [Key]
        public int ID_ESTADO_FACTURA { get; set; }
        public string ESTADO_FAC { get; set; }
        public string IND_ESTADO { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
    }
}