using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_ROLES", Schema = "dbo")]
    public class FC_ROLES
    {
        [Key]
        public int ID_ROL { get; set; }
        public string NOMBRE_ROL { get; set; }   
        public string DESCRIPCION_ROL { get; set; }
        public string IND_ESTADO { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
    }
}