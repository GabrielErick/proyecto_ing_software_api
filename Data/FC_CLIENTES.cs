using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace proyecto_ing_software_api.Data
{
    [Table("FC_CLIENTES", Schema = "dbo")]
    public class FC_CLIENTES
    {
        [Key]
        public int ID_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string DIRECCION { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime? FEC_BAJA { get; set; }
        public string IND_ESTADO { get; set; }
        public string USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public int ID_TIP_TOKEN { get; set; }
        public string TOKEN { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
    }
}