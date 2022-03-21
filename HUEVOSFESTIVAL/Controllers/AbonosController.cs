using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;

namespace HUEVOSFESTIVAL.Controllers
{

    //Tag para bloque sin iniciar session
    [Aceder]
   
    public class AbonosController : Controller
    {
        // GET: Abonos

        public ActionResult Index(ClsVentasCredito oVentasCreditoCls)
        {
            int ClienteParametro = oVentasCreditoCls.idcliente;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsVentasCredito> listaVentasCredito = null;
            //Procedimiento para listar los clientes
            ListarCliente();
            ListarPlaca();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == 0)
                {
                    listaVentasCredito = (from VentaCredito in bd.VENTASCREDITO
                                          where VentaCredito.BHABILITADO == 1
                                          && VentaCredito.CONSECUTIVO != "N/A"
                                           && VentaCredito.MONTODEUDA != 0
                                          select new ClsVentasCredito
                                          {
                                              id = VentaCredito.ID,
                                              consecutivo = VentaCredito.CONSECUTIVO,
                                              idcliente = VentaCredito.IDCLIENTE,
                                              Deuda = VentaCredito.MONTODEUDA

                                          }).ToList();
                }
                else
                {
                    listaVentasCredito = (from VentaCredito in bd.VENTASCREDITO
                                          where VentaCredito.BHABILITADO == 1
                                          && VentaCredito.IDCLIENTE == ClienteParametro
                                            && VentaCredito.CONSECUTIVO != "N/A"
                                             && VentaCredito.MONTODEUDA != 0
                                          select new ClsVentasCredito
                                          {
                                              id = VentaCredito.ID,
                                              consecutivo = VentaCredito.CONSECUTIVO,
                                              idcliente = VentaCredito.IDCLIENTE,
                                              Deuda = VentaCredito.MONTODEUDA
                                          }).ToList();


                }
            }
            return View(listaVentasCredito);
        }

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? ClienteParametro)
        {
            List<ClsVentasCredito> listaVentasCredito = new List<ClsVentasCredito>();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == 0)
                {
                    listaVentasCredito = (from VentaCredito in bd.VENTASCREDITO
                                          where VentaCredito.BHABILITADO == 1
                                          && VentaCredito.CONSECUTIVO != "N/A"
                                           && VentaCredito.MONTODEUDA != 0
                                          select new ClsVentasCredito
                                          {
                                              id = VentaCredito.ID,
                                              consecutivo = VentaCredito.CONSECUTIVO,
                                              idcliente = VentaCredito.IDCLIENTE,
                                              Deuda = VentaCredito.MONTODEUDA

                                          }).ToList();
                }
                else
                {
                    listaVentasCredito = (from VentaCredito in bd.VENTASCREDITO
                                          where VentaCredito.BHABILITADO == 1
                                          && VentaCredito.IDCLIENTE == ClienteParametro
                                          && VentaCredito.CONSECUTIVO != "N/A"
                                           && VentaCredito.MONTODEUDA != 0
                                          select new ClsVentasCredito
                                          {
                                              id = VentaCredito.ID,
                                              consecutivo = VentaCredito.CONSECUTIVO,
                                              idcliente = VentaCredito.IDCLIENTE,
                                              Deuda = VentaCredito.MONTODEUDA

                                          }).ToList();


                }


            }
            return PartialView("_TablaAbonosWeb", listaVentasCredito);

        }

        //cargando el combobox Clientes
        private void ListarCliente()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.CLIENTES
                         where item.BHABILITADO == 1
                         orderby item.CLIENTE ascending
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.CLIENTE,
                             Value = item.IDCLIENTE.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione el Cliente --", Value = "" });
                ViewBag.ListarCliente = Lista;
            }

        }

        //cargando el combobox Placa
        private void ListarPlaca()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.CARROS
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.PLACA,
                             Value = item.IDCARRO.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione la Placa --", Value = "" });
                ViewBag.ListarPlaca = Lista;
            }

        }

        public string Guardar(ClsVentasCredito oVentasCreditoCls, int titulo)
        {
            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new FESTIVALEntities())
                    {
                      
                        using (var transaccion = new TransactionScope())
                        {
                            if (titulo == -1)
                            {
                                //Instanciamos la clase VENTA_PRODWEB del Modelo
                                VENTASCREDITO oVenta = new VENTASCREDITO();
                                oVenta.REMISION = oVentasCreditoCls.Remision;
                                oVenta.NUIP = oVentasCreditoCls.Nuip;
                                oVenta.MONTODEUDA = oVentasCreditoCls.Deuda;
                                oVenta.TIPO = oVentasCreditoCls.Tipo;
                                oVenta.FECHAPAGO = oVentasCreditoCls.fechapago;
                                oVenta.IDCLIENTE = oVentasCreditoCls.idcliente;
                                oVenta.BHABILITADO = 1;
                                oVenta.IDCARRO = oVentasCreditoCls.idcarro;
                                oVenta.CONSECUTIVO = oVentasCreditoCls.consecutivo;
                                bd.VENTASCREDITO.Add(oVenta);
                                rpta = bd.SaveChanges().ToString();
                                if (rpta == "0") rpta = "";
                                transaccion.Complete();
                            }
                            else
                            {
                                VENTASCREDITO oVenta = bd.VENTASCREDITO.Where(p => p.ID == titulo).First();
                                oVenta.REMISION = oVentasCreditoCls.Remision;
                                oVenta.NUIP = oVentasCreditoCls.Nuip;
                                oVenta.MONTODEUDA = oVentasCreditoCls.monto_total;
                                oVenta.TIPO = oVentasCreditoCls.Tipo;
                                oVenta.FECHAPAGO = oVentasCreditoCls.fechapago;
                                oVenta.IDCLIENTE = oVentasCreditoCls.idcliente;
                                oVenta.IDCARRO = oVentasCreditoCls.idcarro;
                                oVenta.CONSECUTIVO = oVentasCreditoCls.consecutivo;
                                rpta = bd.SaveChanges().ToString();
                                transaccion.Complete();
                            }

                        }
                    }

                }
            }
            catch (Exception)
            {
                rpta = "";
            }

            return rpta;

        }

        //Recuperar informacion en funcion del id
        public JsonResult RecuperaInformacion(int idref)
        {
            ClsVentasCredito oVentaCreditoCls = new ClsVentasCredito();
            using (var bd = new FESTIVALEntities())
            {
                VENTASCREDITO oVentaCredito = bd.VENTASCREDITO.Where(p => p.ID == idref).First();
                oVentaCreditoCls.Remision = oVentaCredito.REMISION;
                oVentaCreditoCls.Nuip = oVentaCredito.NUIP;
                oVentaCreditoCls.Deuda = (double)oVentaCredito.MONTODEUDA;
                oVentaCreditoCls.Tipo = oVentaCredito.TIPO;
                oVentaCreditoCls.fechapagocadena = ((DateTime)oVentaCredito.FECHAPAGO).ToString("yyyy-MM-dd");
                oVentaCreditoCls.idcliente = (int)oVentaCredito.IDCLIENTE;
                oVentaCreditoCls.idcarro = (int)oVentaCredito.IDCARRO;
                oVentaCreditoCls.consecutivo = oVentaCredito.CONSECUTIVO;
            }

            return Json(oVentaCreditoCls, JsonRequestBehavior.AllowGet);
        }




    }
}