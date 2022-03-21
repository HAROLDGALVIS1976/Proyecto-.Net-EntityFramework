using HUEVOSFESTIVAL.DAL;
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

    public class CambiosController : Controller
    {
        // GET: Abonos
   
        public ActionResult Index(ClsCambios oCambiosCls)
        {
            int ClienteParametro = oCambiosCls.idcliente;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsCambios> listarCambios = null;
            //Procedimiento para listar los clientes
            ListarUsuario();
            ListarPlaca();
            ListarCliente();
            ListarRuta();
            ListarCarga();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == 0)
                {
                    listarCambios = (from Cambios in bd.CAMBIOS
                                     where Cambios.BHABILITADO == 1
                                     select new ClsCambios
                                     {
                                         id = Cambios.ID,
                                         idcliente = Cambios.IDCLIENTE,
                                         fecha = Cambios.FECHA

                                     }).ToList();
                }
                else
                {
                    listarCambios = (from Cambios in bd.CAMBIOS
                                     where Cambios.BHABILITADO == 1
                                     && Cambios.IDCLIENTE == ClienteParametro
                                     select new ClsCambios
                                     {
                                         id = Cambios.ID,
                                         idcliente = Cambios.IDCLIENTE,
                                         fecha = Cambios.FECHA

                                     }).ToList();


                }
            }
            return View(listarCambios);
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

        //cargando el combobox Clientes
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

        //cargando el combobox Clientes
        private void ListarRuta()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.RUTA
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.NOMBRERUTA,
                             Value = item.IDRUTA.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione la Ruta --", Value = "" });
                ViewBag.ListarRuta = Lista;
            }

        }

        //cargando el combobox Clientes
        private void ListarUsuario()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.USUARIOS
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.USUARIO,
                             Value = item.IDUSUARIO.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione el Usuario --", Value = "" });
                ViewBag.ListarUsuario = Lista;
            }

        }

        //cargando el combobox Clientes
        private void ListarCarga()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.CARGUE
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.IDCARGA.ToString(),
                             Value = item.IDCARGA.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione el IdCarga --", Value = "" });
                ViewBag.ListarCarga = Lista;
            }

        }

        

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? ClienteParametro)
        {
            List<ClsCambios> listarCambios = new List<ClsCambios>();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == 0)
                {
                    listarCambios = (from Cambios in bd.CAMBIOS
                                     where Cambios.BHABILITADO == 1
                                     select new ClsCambios
                                     {
                                         id = Cambios.ID,
                                         idcliente = Cambios.IDCLIENTE,
                                         fecha = Cambios.FECHA

                                     }).ToList();
                }
                else
                {
                    listarCambios = (from Cambios in bd.CAMBIOS
                                     where Cambios.BHABILITADO == 1
                                     && Cambios.IDCLIENTE == ClienteParametro
                                     select new ClsCambios
                                     {
                                         id = Cambios.ID,
                                         idcliente = Cambios.IDCLIENTE,
                                         fecha = Cambios.FECHA

                                     }).ToList();


                }

            }
            return PartialView("_TablaCambios", listarCambios);

        }

        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar(ClsCambios oCambioCls, int titulo)
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
                                transaccion.Complete();
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


        public JsonResult RecuperaInformacion(int id)
        {
            ClsCambios oCambioCls = new ClsCambios();
            using (var bd = new FESTIVALEntities())
            {
                CAMBIOS oCambio = bd.CAMBIOS.Where(p => p.ID == id).First();
                oCambioCls.extr = (int)oCambio.EXTR;
                oCambioCls.aar = (int)oCambio.AAR;
                oCambioCls.ar = (int)oCambio.AR;
                oCambioCls.br = (int)oCambio.BR;
                oCambioCls.extb = (int)oCambio.EXTB;
                oCambioCls.aab = (int)oCambio.AAB;
                oCambioCls.ab = (int)oCambio.AB;
                oCambioCls.bb = (int)oCambio.BB;
                oCambioCls.idusuario = (int)oCambio.IDUSUARIO;
                oCambioCls.idcarro = (int)oCambio.IDCARRO;
                oCambioCls.idcliente = (int)oCambio.IDCLIENTE;
                oCambioCls.idruta = (int)oCambio.IDRUTA;
                oCambioCls.Fechacadena = ((DateTime)oCambio.FECHA).ToString("yyyy-MM-dd");
                oCambioCls.usuario = oCambio.USUARIO;
                oCambioCls.placa = oCambio.PLACA;
                oCambioCls.idcarga = (int)oCambio.IDCARGA;
            }

            return Json(oCambioCls, JsonRequestBehavior.AllowGet);
        }


        //Procedimiento JSON para hallar el IdRuta en funcion del IdCliente
        public JsonResult RecuperarRuta(int id)
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
              
                oCargueCls.idusuario = (int)oCargue.IDUSUARIO;
                oCargueCls.idcarro = (int)oCargue.IDCARRO;

            }

            return Json(oCargueCls, JsonRequestBehavior.AllowGet);
        }


        public int Elimina(int id)
        {
            int nregistrosAfectados = 0;
            try
            {
                using (var bd = new FESTIVALEntities())
                {
                    CAMBIOS oCambio = bd.CAMBIOS.Where(p => p.ID == id).First();
                    oCambio.BHABILITADO = 0;
                    nregistrosAfectados = bd.SaveChanges();
                }

            }
            catch (Exception)
            {

                nregistrosAfectados = 0;
            }

            return nregistrosAfectados;
        }








    }
}