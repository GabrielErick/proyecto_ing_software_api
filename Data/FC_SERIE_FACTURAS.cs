using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_SERIE_FACTURAS", Schema = "dbo")]
    public class FC_SERIE_FACTURAS
    {
        [Key]
        public int ID_SERIE { get; set; }
        public string SERIE { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
        public string IND_ESTADO { get; set; }
    }
}