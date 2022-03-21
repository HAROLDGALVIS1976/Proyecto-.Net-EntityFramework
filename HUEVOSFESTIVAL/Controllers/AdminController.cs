using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using HUEVOSFESTIVAL.Models.Home;

namespace HUEVOSFESTIVAL.Controllers
{
    //Tag para bloquear vistas sin iniciar session
    [Aceder]
    public class AdminController : Controller
    {
 


        public ActionResult Index(ClsVentasweb oVentasCls)
        {
            int idcliente = oVentasCls.idcliente;
            /* Creacion de la ListaClientes para mostrar en <tabla> en la vista Index de Clientes)*/
            List<ClsVentasweb> listaDeuda = null;
            ListarRutas();
            ListarClientes();
            ListarTipo();
            ListarRef();
            ListarUsuarios();
            ListarPlaca();
            ListarCarga();
            using (var  bd = new FESTIVALEntities())
            {

                listaDeuda = (from Venta in bd.VENTA_PRODWEB
                              join cliente in bd.CLIENTES
                              on Venta.IDCLIENTE equals cliente.IDCLIENTE
                              join referencia in bd.REFERENCIAS
                               on Venta.IDREF equals referencia.IDREF
                              where Venta.BHABILITADO == 1
                              && Venta.IDCLIENTE == idcliente
                              select new ClsVentasweb
                              {
                                  id = Venta.ID,
                                  cliente = cliente.CLIENTE,
                                  Ref = referencia.DESCRIPCION,
                                  monto_total = Venta.MONTOVENTA

                              }).ToList();


            }
            return View(listaDeuda);
        }

        private void ListarCarga()
        {
            List<SelectListItem> ListaCarga = null;
            using (var bd = new FESTIVALEntities())
            {
                ListaCarga = (from item in bd.CARGUE
                              where item.BHABILITADO == 1
                              select new SelectListItem
                              {
                                  /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                  Text = item.IDCARGA.ToString(),
                                  Value = item.IDCARGA.ToString()
                              }).ToList();
                ListaCarga.Insert(0, new SelectListItem { Text = "-- Seleccione el Identificador --", Value = "" });
                ViewBag.ListarCarga = ListaCarga;
            }


        }
        //cargando el combobox Rutas
        private void ListarRutas()
        {
            List<SelectListItem> Listaruta = null;
            using (var bd = new FESTIVALEntities())
            {
                Listaruta = (from item in bd.RUTA
                             where item.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                 Text = item.NOMBRERUTA,
                                 Value = item.IDRUTA.ToString()
                             }).ToList();
                Listaruta.Insert(0, new SelectListItem { Text = "-- Seleccione la Ruta --", Value = "" });
                ViewBag.ListarRutas = Listaruta;
            }


        }
        //cargando el combobox Tipo Venta
        private void ListarClientes()
        {
            List<SelectListItem> Listacliente = null;
            using (var bd = new FESTIVALEntities())
            {
                Listacliente = (from item in bd.CLIENTES
                                where item.BHABILITADO == 1
                                orderby item.CLIENTE ascending
                                select new SelectListItem
                                {
                                    /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                    Text = item.CLIENTE,
                                    Value = item.IDCLIENTE.ToString()
                                }).ToList();
                Listacliente.Insert(0, new SelectListItem { Text = "-- Seleccione el Cliente --", Value = "" });
                ViewBag.ListarClientes = Listacliente;
            }

        }
        //cargando el combobox ListaTipos
        private void ListarTipo()
        {
            List<SelectListItem> Listatipo = null;
            using (var bd = new FESTIVALEntities())
            {
                Listatipo = (from item in bd.Tbl_TIPO
                             where item.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                 Text = item.TIPOVENTA,
                                 Value = item.IDTIPOVENTA.ToString()
                             }).ToList();
                Listatipo.Insert(0, new SelectListItem { Text = "-- Seleccione Tipo Venta --", Value = "" });
                ViewBag.ListarTipo = Listatipo;
            }

        }
        //cargando el combobox referencia
        private void ListarRef()
        {
            List<SelectListItem> Listaref = null;
            using (var bd = new FESTIVALEntities())
            {
                Listaref = (from item in bd.REFERENCIAS
                            where item.BHABILITADO == 1
                            select new SelectListItem
                            {
                                /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                Text = item.REF,
                                Value = item.IDREF.ToString()
                            }).ToList();
                Listaref.Insert(0, new SelectListItem { Text = "-- Seleccione la Referencia --", Value = "" });
                ViewBag.ListarRef = Listaref;
            }

        }
        //cargando el combobox referencia
        private void ListarUsuarios()
        {
            List<SelectListItem> Listausuario = null;
            using (var bd = new FESTIVALEntities())
            {
                Listausuario = (from item in bd.USUARIOS
                                where item.BHABILITADO == 1

                                select new SelectListItem
                                {
                                    /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                    Text = item.USUARIO,
                                    Value = item.IDUSUARIO.ToString()
                                }).ToList();
                Listausuario.Insert(0, new SelectListItem { Text = "-- Seleccione el Usuario --", Value = "" });
                ViewBag.ListarUsuarios = Listausuario;
            }

        }
        //cargando el combobox referencia
        private void ListarPlaca()
        {
            List<SelectListItem> Listaplaca = null;
            using (var bd = new FESTIVALEntities())
            {
                Listaplaca = (from item in bd.CARROS
                              where item.BHABILITADO == 1
                              select new SelectListItem
                              {
                                  /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                                  Text = item.PLACA,
                                  Value = item.IDCARRO.ToString()
                              }).ToList();
                Listaplaca.Insert(0, new SelectListItem { Text = "-- Seleccione la Placa --", Value = "" });
                ViewBag.ListarPlaca = Listaplaca;
            }

        }

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? RutaParametro)
        {
            List<ClsVentasweb> listaDeuda = new List<ClsVentasweb>();
            using (var bd = new FESTIVALEntities())
            {

                listaDeuda = (from Venta in bd.VENTA_PRODWEB
                              join cliente in bd.CLIENTES
                              on Venta.IDCLIENTE equals cliente.IDCLIENTE
                              join referencia in bd.REFERENCIAS
                               on Venta.IDREF equals referencia.IDREF
                              where Venta.BHABILITADO == 1
                              && Venta.IDCLIENTE == RutaParametro
                              select new ClsVentasweb
                              {
                                  id = Venta.ID,
                                  cliente = cliente.CLIENTE,
                                  Ref = referencia.DESCRIPCION,
                                  monto_total = Venta.MONTOVENTA

                              }).ToList();

            }
            return PartialView("_TablaVentaweb", listaDeuda);

        }

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult FiltrarReferencias(int? Parametro, int? idreferencia)
        {
            List<ClsVentasweb> listaRef = new List<ClsVentasweb>();
            using (var bd = new FESTIVALEntities())
            {

                if (Parametro == 0 && idreferencia == 0)
                {
                    listaRef = (from Venta in bd.VENTA_PRODWEB
                                     where Venta.BHABILITADO == 0
                                     orderby Venta.CANT descending
                                     select new ClsVentasweb
                                     {
                                         idcarga = Venta.IDCARGA,
                                         idref = Venta.IDREF,
                                         cliente = Venta.CLIENTE,
                                         Ref = Venta.REF,
                                         cant = Venta.CANT
                                     }).ToList();
                }
                else
                {
                    listaRef = (from Venta in bd.VENTA_PRODWEB
                                  where Venta.BHABILITADO == 1
                                  && Venta.IDCARGA == Parametro
                                  && Venta.IDREF == idreferencia
                                  orderby Venta.CANT descending
                                  select new ClsVentasweb
                                  {
                                      idcarga = Venta.IDCARGA,
                                      idref = Venta.IDREF,
                                      cliente = Venta.CLIENTE,
                                      Ref = Venta.REF,
                                      cant = Venta.CANT
                                  }).ToList();
                }


            }
            return PartialView("_TablaTotalesCarga", listaRef);

        }


        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar(ClsVentasweb oVentasCls, int titulo)
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
                                VENTA_PRODWEB oVenta = new VENTA_PRODWEB();
                                oVenta.IDREF = oVentasCls.idref;
                                oVenta.IDCLIENTE = oVentasCls.idcliente;
                                oVenta.IDRUTA = oVentasCls.idruta;
                                oVenta.IDTIPOVENTA = oVentasCls.idtipoventa;
                                oVenta.IDCARRO = oVentasCls.idcarro;
                                oVenta.IDUSUARIO = oVentasCls.idusuario;
                                oVenta.REF = oVentasCls.Ref;
                                oVenta.CLIENTE = oVentasCls.cliente;
                                oVenta.RUTA = oVentasCls.ruta;
                                oVenta.TIPO = oVentasCls.tipo;
                                oVenta.VALUNI_VENTA = oVentasCls.valuni_venta;
                                oVenta.CANT = oVentasCls.cant;
                                oVenta.MONTO_TOTAL = oVentasCls.monto_total;
                                oVenta.FECHAVENTA = oVentasCls.fechaventa;
                                oVenta.FECHAPAGO = oVentasCls.fechapago;
                                oVenta.BHABILITADO = 1;
                                oVenta.PLACA = oVentasCls.placa;
                                oVenta.CONSECUTIVO = oVentasCls.consecutivo;
                                oVenta.IDCARGA = oVentasCls.idcarga;
                                oVenta.ABONO = oVentasCls.abono;
                                bd.VENTA_PRODWEB.Add(oVenta);
                                rpta = bd.SaveChanges().ToString();
                                if (rpta == "0") rpta = "";
                                transaccion.Complete();

                            }
                            else
                            {
                                VENTA_PRODWEB oVenta = bd.VENTA_PRODWEB.Where(p => p.ID == titulo).First();
                                oVenta.IDREF = oVentasCls.idref;
                                oVenta.IDCLIENTE = oVentasCls.idcliente;
                                oVenta.IDRUTA = oVentasCls.idruta;
                                oVenta.IDTIPOVENTA = oVentasCls.idtipoventa;
                                oVenta.IDCARRO = oVentasCls.idcarro;
                                oVenta.IDUSUARIO = oVentasCls.idusuario;
                                oVenta.REF = oVentasCls.Ref;
                                oVenta.CLIENTE = oVentasCls.cliente;
                                oVenta.RUTA = oVentasCls.ruta;
                                oVenta.TIPO = oVentasCls.tipo;
                                oVenta.VALUNI_VENTA = oVentasCls.valuni_venta;
                                oVenta.CANT = oVentasCls.cant;
                                oVenta.MONTO_TOTAL = oVentasCls.monto_total;
                                oVenta.FECHAVENTA = oVentasCls.fechaventa;
                                oVenta.FECHAPAGO = oVentasCls.fechapago;
                                oVenta.PLACA = oVentasCls.placa;
                                oVenta.CONSECUTIVO = oVentasCls.consecutivo;
                                oVenta.IDCARGA = oVentasCls.idcarga;
                                oVenta.ABONO = oVentasCls.abono;
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

        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar2(ClsCambios oCambioCls, int titulo)
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
                        if (titulo == -1)
                        {
                            //Instanciamos la clase VENTA_PRODWEB del Modelo
                            CAMBIOS oCambio = new CAMBIOS();
                            oCambio.EXTR = oCambioCls.extr;
                            oCambio.AAR = oCambioCls.aar;
                            oCambio.AR = oCambioCls.ar;
                            oCambio.BR = oCambioCls.br;
                            oCambio.EXTB = oCambioCls.extb;
                            oCambio.AAB = oCambioCls.aab;
                            oCambio.AB = oCambioCls.ab;
                            oCambio.BB = oCambioCls.bb;
                            oCambio.IDUSUARIO = oCambioCls.idusuario;
                            oCambio.IDCARRO = oCambioCls.idcarro;
                            oCambio.IDCLIENTE = oCambioCls.idcliente;
                            oCambio.IDRUTA = oCambioCls.idruta;
                            oCambio.FECHA = oCambioCls.fecha;
                            oCambio.BHABILITADO = 1;
                            oCambio.USUARIO = oCambioCls.usuario;
                            oCambio.PLACA = oCambioCls.placa;
                            oCambio.IDCARGA = oCambioCls.idcarga;
                            bd.CAMBIOS.Add(oCambio);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            //Editando en la tabla VENTASCREDITO
                            CAMBIOS oCambio = bd.CAMBIOS.Where(p => p.ID == titulo).First();
                            oCambio.EXTR = oCambioCls.extr;
                            oCambio.AAR = oCambioCls.aar;
                            oCambio.AR = oCambioCls.ar;
                            oCambio.BR = oCambioCls.br;
                            oCambio.EXTB = oCambioCls.extb;
                            oCambio.AAB = oCambioCls.aab;
                            oCambio.AB = oCambioCls.ab;
                            oCambio.BB = oCambioCls.bb;
                            oCambio.IDUSUARIO = oCambioCls.idusuario;
                            oCambio.IDCARRO = oCambioCls.idcarro;
                            oCambio.IDCLIENTE = oCambioCls.idcliente;
                            oCambio.IDRUTA = oCambioCls.idruta;
                            oCambio.FECHA = oCambioCls.fecha;
                            oCambio.USUARIO = oCambioCls.usuario;
                            oCambio.PLACA = oCambioCls.placa;
                            oCambio.IDCARGA = oCambioCls.idcarga;
                            rpta = bd.SaveChanges().ToString();
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





        //Procedimiento para Recuperar la informacion de la tabla
        // VentasWeb en funcion del idref
        public JsonResult RecuperarInformacion(int idventa)
        {
            ClsVentasweb oVentaCls = new ClsVentasweb();
            using (var bd = new FESTIVALEntities())
            {
                VENTA_PRODWEB oVenta = bd.VENTA_PRODWEB.Where(p => p.ID == idventa).First();
                oVentaCls.idref = (int)oVenta.IDREF;
                oVentaCls.idcliente = (int)oVenta.IDCLIENTE;
                oVentaCls.idruta = (int)oVenta.IDRUTA;
                oVentaCls.idtipoventa = (int)oVenta.IDTIPOVENTA;
                oVentaCls.idcarro = (int)oVenta.IDCARRO;
                oVentaCls.idusuario = (int)oVenta.IDUSUARIO;
                oVentaCls.Ref = oVenta.REF;
                oVentaCls.cliente = oVenta.CLIENTE;
                oVentaCls.ruta = oVenta.RUTA;
                oVentaCls.tipo = oVenta.TIPO;
                oVentaCls.valuni_venta = (double)oVenta.VALUNI_VENTA;
                oVentaCls.cant = (double)oVenta.CANT;
                oVentaCls.monto_total = (double)oVenta.MONTO_TOTAL;
                oVentaCls.fechaVentaCadena = ((DateTime)oVenta.FECHAVENTA).ToString("yyyy-MM-dd");
                oVentaCls.fechaPagoCadena = ((DateTime)oVenta.FECHAPAGO).ToString("yyyy-MM-dd");
                oVentaCls.placa = oVenta.PLACA;
                oVentaCls.consecutivo = oVenta.CONSECUTIVO;
                oVentaCls.idcarga = (int)oVenta.IDCARGA;
                oVentaCls.abono = (double)oVenta.ABONO;

            }

            return Json(oVentaCls, JsonRequestBehavior.AllowGet);
        }


        //Procedimiento para Recuperar el valor de la Referencia
        // en funcion del idref
        public JsonResult BuscaReferencia(int id)
        {
            ClsReferencias oReferenciasCls = new ClsReferencias();
            using (var bd = new FESTIVALEntities())
            {
                REFERENCIAS oReferencia = bd.REFERENCIAS.Where(p => p.IDREF == id).First();        
                oReferenciasCls.valmin_venta = (double)oReferencia.VALMIN_VENTA;
                oReferenciasCls.valuni_venta = (double)oReferencia.VALUNI_VENTA;

            }

            return Json(oReferenciasCls, JsonRequestBehavior.AllowGet);
        }


        //Procedimiento JSON para hallar el IdRuta en funcion del IdCliente
        public JsonResult BuscaRuta(int id)
        {
            ClsClientes oClientesCls = new ClsClientes();
            using (var bd = new FESTIVALEntities())
            {
                CLIENTES oCliente = bd.CLIENTES.Where(p => p.IDCLIENTE == id).First();
                oClientesCls.idruta = (int)oCliente.IDRUTA;

            }

            return Json(oClientesCls, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaCarga(int idcarga)
        {
            ClsCargue oCargueCls = new ClsCargue();
            using (var bd = new FESTIVALEntities())
            {
                CARGUE2 oCargue = bd.CARGUE2.Where(p => p.IDCARGA == idcarga).First();
                oCargueCls.extr = (int)oCargue.EXTR;
                oCargueCls.aar = (int)oCargue.AAR;
                oCargueCls.ar = (int)oCargue.AR;
                oCargueCls.br = (int)oCargue.BR;
                oCargueCls.extb = (int)oCargue.EXTB;
                oCargueCls.aab = (int)oCargue.AAB;
                oCargueCls.ab = (int)oCargue.AB;
                oCargueCls.bb = (int)oCargue.BB;
                oCargueCls.idusuario = (int)oCargue.IDUSUARIO;
                oCargueCls.idcarro = (int)oCargue.IDCARRO;

            }

            return Json(oCargueCls, JsonRequestBehavior.AllowGet);
        }


        public int EliminarViaje(int idventa)
        {
            int nregistrosAfectados = 0;
            try
            {
                using (var bd = new FESTIVALEntities())
                {
                    VENTA_PRODWEB oVenta = bd.VENTA_PRODWEB.Where(p => p.ID == idventa).First();
                    oVenta.BHABILITADO = 0;
                    nregistrosAfectados = bd.SaveChanges();
                }

            }
            catch (Exception)
            {

                nregistrosAfectados = 0;
            }

            return nregistrosAfectados;
        }


        //cargando el combobox Rutas de Ventas



        public ActionResult Agregar()
        {
            ListarRutas();
            ListarClientes();
            ListarTipo();
            ListarRef();
            ListarUsuarios();
            ListarPlaca();
            return View();
        }

        //ActionResult para Guardar los datos
        [HttpPost]
        public ActionResult Agregar(ClsVentasweb OClsVentaWeb)
        {
            if (!ModelState.IsValid)
            {
                ListarRutas();
                ListarClientes();
                ListarTipo();
                ListarRef();
                ListarUsuarios();
                ListarPlaca();
                return View(OClsVentaWeb);
            }
            else
            {
                using (var bd = new FESTIVALEntities())
                {
                    VENTA_PRODWEB oVentaWeb = new VENTA_PRODWEB();
                    oVentaWeb.IDREF = OClsVentaWeb.idref;
                    oVentaWeb.IDCLIENTE = OClsVentaWeb.idcliente;
                    oVentaWeb.IDRUTA = OClsVentaWeb.idruta;
                    oVentaWeb.IDTIPOVENTA = OClsVentaWeb.idtipoventa;
                    oVentaWeb.IDCARRO = OClsVentaWeb.idcarro;
                    oVentaWeb.IDUSUARIO = OClsVentaWeb.idusuario;
                    oVentaWeb.REF = OClsVentaWeb.Ref;
                    oVentaWeb.CLIENTE = OClsVentaWeb.cliente;
                    oVentaWeb.RUTA = OClsVentaWeb.ruta;
                    oVentaWeb.TIPO = OClsVentaWeb.tipo;
                    oVentaWeb.VALUNI_VENTA = OClsVentaWeb.valuni_venta;
                    oVentaWeb.CANT = OClsVentaWeb.cant;
                    oVentaWeb.MONTO_TOTAL = OClsVentaWeb.monto_total;
                    oVentaWeb.FECHAVENTA = OClsVentaWeb.fechaventa;
                    oVentaWeb.FECHAPAGO = OClsVentaWeb.fechapago;
                    oVentaWeb.BHABILITADO = 1;
                    oVentaWeb.PLACA = OClsVentaWeb.placa;
                    oVentaWeb.CONSECUTIVO = OClsVentaWeb.consecutivo;
                    bd.VENTA_PRODWEB.Add(oVentaWeb);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Admin");

        }


       



    }
}