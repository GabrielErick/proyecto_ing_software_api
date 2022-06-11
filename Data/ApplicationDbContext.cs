using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace proyecto_ing_software_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FC_CLIENTES> FC_CLIENTES { get; set; }
        public DbSet<FC_ESTADO_FACTURA> FC_ESTADO_FACTURA { get; set; }
        public DbSet<FC_ROLES> FC_ROLES { get; set; }
        public DbSet<FC_SERIE_FACTURAS> FC_SERIE_FACTURAS { get; set; }
        public DbSet<FC_TIENDAS_CLIENTES> FC_TIENDAS_CLIENTES { get; set; }
        public DbSet<FC_TIPO_TOKEN> FC_TIPO_TOKEN { get; set; }
        public DbSet<FC_USUARIOS_ROLES> FC_USUARIOS_ROLES { get; set; }
        public DbSet<FC_USUARIOS> FC_USUARIOS { get; set; }
        public DbSet<FC_FACTURA_ENCABEZADO> FC_FACTURA_ENCABEZADO { get; set; }
        public DbSet<FC_FACTURA_DETALLE> FC_FACTURA_DETALLE { get; set; }
    }
}
