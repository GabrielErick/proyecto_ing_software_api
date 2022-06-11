using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_ing_software_api.Data
{
    [Table("FC_TIENDAS_CLIENTES", Schema = "dbo")]
    public class FC_TIENDAS_CLIENTES
    {
        [Key]
        public int ID_TIENDA_CLIENTE { get; set; }
        public int ID_CLIENTE { get; set; }
        public int NUM_TIENDA_CLIENTE { get; set; }
        public int ID_TIP_TOKEN { get; set; }
        public string TOKEN { get; set; }
        public DateTime FEC_CREACION { get; set; }
        public DateTime FEC_BAJA { get; set; }
        public string IND_ESTADO { get; set; }
    }
}