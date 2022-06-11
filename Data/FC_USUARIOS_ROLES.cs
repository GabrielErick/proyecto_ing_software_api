using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_USUARIOS_ROLES", Schema = "dbo")]
    public class FC_USUARIOS_ROLES
    {
        [Key]
        public int ID_USUARIO_ROL { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_ROL { get; set; }
        public string   IND_ESTADO { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
    }
}