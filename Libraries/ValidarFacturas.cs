using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proyecto_ing_software_api.Data;
using proyecto_ing_software_api.Models;
using System.Linq;

namespace proyecto_ing_software_api.Libraries
{
    public class ValidarFacturas
    {
        // *! LLAMAMOS A LA BASE DE DATOS PAR USARLA EN LA CLASE
        private ApplicationDbContext context;
        public ValidarFacturas(ApplicationDbContext _context){
            context = _context;
        }



        public ResponseResult validarFacturaNueva(ModelNewFac newFactura){
            var response = new ResponseResult();
            response.estado = true;
            response.mensaje = "correcto";

            var infoCliente = (from cliente in context.FC_CLIENTES // *! CONSULTA AL CLINTE
                                where cliente.ID_CLIENTE.Equals(newFactura.idCliente)
                                select new FC_CLIENTES{
                                    NOMBRE_CLIENTE = cliente.NOMBRE_CLIENTE,
                                    ID_CLIENTE = cliente.ID_CLIENTE,
                                    DIRECCION = cliente.DIRECCION,
                                    FEC_BAJA = cliente.FEC_BAJA,
                                    IND_ESTADO = cliente.IND_ESTADO,
                                    USUARIO = cliente.USUARIO,
                                    FEC_CREACION = cliente.FEC_CREACION,
                                    PASSWORD = cliente.PASSWORD,
                                    TOKEN = cliente.TOKEN,
                                    CORREO = cliente.CORREO,
                                    TELEFONO = cliente.TELEFONO
                                }).FirstOrDefault();

            if(infoCliente == null){ // *! SI EL NO CLINTE EXISTE
                response.estado = false;
                    response.mensaje = "El codigo de cliente no es correcto.";
            }

            var infoFactura = (from factura in context.FC_FACTURA_ENCABEZADO
                                where factura.ID_SERIE.Equals(newFactura.idSerie) && factura.ID_CLIENTE.Equals(newFactura.idCliente)
                                select factura).FirstOrDefault();
            
            if(infoFactura == null){
                response.estado = false;
                    response.mensaje = "El codigo de serie de la factura no es correcto.";
            }

            

            try{
                if(newFactura == null){
                    response.estado = false;
                    response.mensaje = "El modelo de datos esta vacio.";
                }

                if(newFactura.detalleFactura == null || newFactura.detalleFactura.Length == 0){
                    response.estado = false;
                    response.mensaje = "El detalle de la factura esta vacio.";
                }

                if(newFactura.idSerie == 0){
                    response.estado = false;
                    response.mensaje = "La Serie está vacia.";
                }

                if(newFactura.idCliente == 0){
                    response.estado = false;
                    response.mensaje = "El Cliente está vacio.";
                }

                if(newFactura.idTiendaCliente == 0){
                    response.estado = false;
                    response.mensaje = "La Tienda del Cliente esta vacia.";
                }

                if(newFactura.nombreClienteFinal.Length == 0){
                    response.estado = false;
                    response.mensaje = "El Nombre de Cliente Final está vacio.";
                }

                if(newFactura.direccionClienteFinal.Length == 0){
                    response.estado = false;
                    response.mensaje = "La dirección del cliente final está vacia.";
                }

                if(newFactura.nitClienteFinal.Length == 0){
                    response.estado = false;
                    response.mensaje = "El nit del cliente final está vacio.";
                }

                if(newFactura.totalFactura == 0){
                    response.estado = false;
                    response.mensaje = "El total de la factura está vacia.";
                }
                
                int l = 1;
                decimal totalDocumento = 0;
                foreach(var row in newFactura.detalleFactura){


                    if(row.numLinea == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", El numero de linea está vacio.";
                    }

                    if(row.descripcion.Length == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", la descripcion está vacia.";
                    }

                    if(row.precioUnitario == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", el precio unitario está vacia.";
                    }

                    if(row.iva == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", el iva está vacio.";
                    }

                    if(row.precioVenta == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", el precio de venta está vacio.";
                    }

                    if(row.cantidad == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", la cantidad está vacia.";
                    }
                    
                    if(row.totalLinea == 0){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", el total está vacio.";
                    }

                    decimal totalRow = row.precioUnitario + row.iva;
                    if(row.precioVenta != totalRow){
                        response.estado = false;
                        response.mensaje = "Error en la linea " + l.ToString() +", error en el total de la linea.";
                    }
                    
                    totalDocumento += row.totalLinea;

                    l++;
                }

                if(totalDocumento != newFactura.totalFactura){
                    response.estado = false;
                    response.mensaje = "Error el total dela factura no es igual total del detalle.";
                }

            }catch(Exception ex){
                response.estado = false;
                response.mensaje = "Error1: " + ex.Message + "\nError2: " + ex.InnerException.Message;
            }
            return response;
        }


        public async Task<ResponseResult> guardarFacturaAsync(ModelNewFac newFactura){
            var response = new ResponseResult();
            var estrategia = context.Database.CreateExecutionStrategy();
            
            var tokenUnico = string.Empty;
            bool esunico = false;



            
            do{
                tokenUnico = TokenGenerador.GenerarToken();
                var tokens = await (from tkn in context.FC_FACTURA_ENCABEZADO
                             where tkn.TOKEN.Equals(tokenUnico)
                             select new FC_FACTURA_ENCABEZADO{
                                 TOKEN = tkn.TOKEN
                             }).ToListAsync();
                
                if(tokens.Count == 0){
                    esunico = true;
                }
            }while(!esunico);






            try{
                var serieCliente = await (from sri in context.FC_SERIE_FACTURAS
                    where sri.ID_SERIE.Equals(newFactura.idSerie)
                    select new FC_SERIE_FACTURAS{
                        SERIE = sri.SERIE
                    }).FirstOrDefaultAsync();

                await estrategia.ExecuteAsync( async() => {
                    using(var trnEnc = await context.Database.BeginTransactionAsync()){
                        try{
                            var factura = new FC_FACTURA_ENCABEZADO{
                                CORRELATIVO_CLIENTE = newFactura.numFacturaLocal,
                                CORREO_CONTRIBUYENTE = newFactura.correoClienteFinal,
                                DIRECCION_CONTRIBUYENTE = newFactura.direccionClienteFinal,
                                FEC_CREACION = DateTime.Now,
                                ID_CLIENTE = newFactura.idCliente,
                                ID_ESTADO_FACTURA = 1000,
                                ID_SERIE = newFactura.idSerie,
                                ID_TIENDA_CLIENTE = newFactura.idTiendaCliente,
                                ID_TIP_TOKEN = 1000,
                                IND_ESTADO = "A",
                                NIT_CONTRIBUYENTE  = newFactura.nitClienteFinal,
                                NOMBRE_CONTRIBUYENTE = newFactura.nombreClienteFinal,
                                TOKEN = tokenUnico,
                                TOTAL_FACTURA = newFactura.totalFactura
                            };
                            await context.FC_FACTURA_ENCABEZADO.AddAsync(factura);
                            await context.SaveChangesAsync();
                            await trnEnc.CommitAsync();
                            response.estado = true;
                            response.mensaje = "{token : " + tokenUnico + ", serie: " +  serieCliente.SERIE + "}";
                        }catch(Exception ex){
                            await trnEnc.RollbackAsync();
                            response.estado = false;
                            response.mensaje = "Error 1: " + ex.Message + "\n Error 2:" + ex.InnerException.Message;
                        }
                    }    
                });

                await estrategia.ExecuteAsync( async () => {
                    if(response.estado){
                        using(var trnDet = await context.Database.BeginTransactionAsync()){
                            var lastRow = await context.FC_FACTURA_ENCABEZADO.OrderBy(or => or.ID_FAC_ENCABEZADO).LastOrDefaultAsync();
                            try{
                                var listDetalle = new List<FC_FACTURA_DETALLE>();
                                foreach(var rowDetalle in newFactura.detalleFactura){
                                    var row = new FC_FACTURA_DETALLE{
                                        CANTIDAD = rowDetalle.cantidad,
                                        DESCRIPCION = rowDetalle.descripcion,
                                        DESCUENTO_TOTAL = rowDetalle.descuentoTotal,
                                        DESCUENTO_UNIDAD = rowDetalle.descuentoUnitario,
                                        IVA = rowDetalle.iva,
                                        NUM_LINEA = rowDetalle.numLinea,
                                        PRECIO_UNITARIO = rowDetalle.precioUnitario,
                                        PRECIO_VENTA = rowDetalle.precioVenta,
                                        TOTAL_LINEA = rowDetalle.totalLinea,
                                        ID_FAC_ENCABEZADO = lastRow.ID_FAC_ENCABEZADO
                                    };

                                    listDetalle.Add(row);
                                }

                                await context.FC_FACTURA_DETALLE.AddRangeAsync(listDetalle);
                                await context.SaveChangesAsync();
                                await trnDet.CommitAsync();
                            }catch(Exception ex){
                                await trnDet.RollbackAsync();
                                context.FC_FACTURA_ENCABEZADO.Remove(lastRow);                        
                                response.estado = false;
                                response.mensaje = "Error 1: " + ex.Message + "\n Error 2:" + ex.InnerException.Message;
                            }
                        }
                    }

                });
            }catch(Exception ex){
                var lastRow = await context.FC_FACTURA_ENCABEZADO.OrderBy(or => or.ID_FAC_ENCABEZADO).LastOrDefaultAsync();
                var lastDet = await context.FC_FACTURA_DETALLE.Where(w => w.ID_FAC_ENCABEZADO.Equals(lastRow.ID_FAC_ENCABEZADO)).ToListAsync();
                context.FC_FACTURA_DETALLE.RemoveRange(lastDet);
                context.FC_FACTURA_ENCABEZADO.Remove(lastRow);
                response.mensaje = ex.Message;
                response.estado = false;
            }
            

            return response;
        }


        public async Task<ResponseResult> AnularFacturaAsync(string token){
            var response = new ResponseResult();
            var estrategia = context.Database.CreateExecutionStrategy();
            try{
                var facturaAnular = await context.FC_FACTURA_ENCABEZADO.Where(w => w.TOKEN.Equals(token)).FirstOrDefaultAsync();
                await estrategia.ExecuteAsync(async () =>{
                    using(var tran = await context.Database.BeginTransactionAsync()){
                        try{
                            facturaAnular.FEC_BAJA = DateTime.Now;
                            facturaAnular.ID_ESTADO_FACTURA = 1001;
                            facturaAnular.IND_ESTADO = "N";

                            context.FC_FACTURA_ENCABEZADO.Update(facturaAnular);
                            await context.SaveChangesAsync();
                            await tran.CommitAsync();
                            response.estado = true;
                            response.mensaje = "Factura Anulada.";
                        }catch(Exception ex){
                            await tran.RollbackAsync();
                            response.estado = false;
                            response.mensaje = "Error1: La factura no se pudo anlar. \nError2: " + ex.Message+ ". \nError3: " + ex.InnerException.Message;
                        }
                    }
                });
                
            }catch(Exception ex){
                response.estado = false;
                response.mensaje = "Error1: " + ex.Message+ ". \nError2: " + ex.InnerException.Message;
            }
            return response;
        }
        
    }
}