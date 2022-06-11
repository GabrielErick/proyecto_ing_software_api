using System;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_TIPO_TOKEN", Schema = "dbo")]
    public class FC_TIPO_TOKEN
    {
        [Key]
        public int ID_TIP_TOKEN { get; set; }
        public string TIPO_TOKEN { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
        public string IND_ESTADO { get; set; }
    }
}