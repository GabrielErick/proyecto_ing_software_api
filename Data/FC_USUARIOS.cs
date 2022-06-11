using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_USUARIOS", Schema = "dbo")]
    public class FC_USUARIOS
    {
        [Key]
        public int ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string PASSWORD { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
        public string CORREO { get; set; }
        public int ID_TIP_TOKEN { get; set; }
        public string TOKEN { get; set; }
    }
}