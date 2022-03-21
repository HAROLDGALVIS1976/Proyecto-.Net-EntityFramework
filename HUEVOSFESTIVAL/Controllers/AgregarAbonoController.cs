using HUEVOSFESTIVAL.DAL;
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;

namespace HUEVOSFESTIVAL.Controllers
{
    public class AgregarAbonoController : Controller
    {

        //Tag para bloquear vistas sin iniciar session
        [Aceder]

        // GET: AgregarAbono

        public ActionResult Index(ClsAbonosVentasCredito oAgregarabonoCls)
        {
            int Cliente = oAgregarabonoCls.idcliente;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsAbonosVentasCredito> listabono = null;
            //Lista para llenar el DropDownList Remision
            ListaConsecutivo();
            ListarCliente();
            ListarUsuario();
            ListarPlaca();
            using (var bd = new FESTIVALEntities())
            {
                /*si el idref == 0 cuando no hay seleccion en el helper  @Html.DropDownList de la vista Index ==>*/
                if (Cliente == 0)
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.BHABILITADO == 1
                                
                                 select new ClsAbonosVentasCredito
                                 {
                                     ID = Abono.ID,
                                     consecutivo = Abono.CONSECUTIVO,
                                     idcliente = Abono.IDCLIENTE,
                                     abono = Abono.ABONO,
                                     fechabono = Abono.FECHABONO

                                 }).ToList();
                }
                else
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.BHABILITADO == 1
                                 && Abono.IDCLIENTE == Cliente
                                 select new ClsAbonosVentasCredito
                                 {
                                     ID = Abono.ID,
                                     consecutivo = Abono.CONSECUTIVO,
                                     idcliente = Abono.IDCLIENTE,
                                     abono = Abono.ABONO,
                                     fechabono = Abono.FECHABONO

                                 }).ToList();


                }
            }
            return View(listabono);
        }


        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? ClienteParametro)
        {
            List<ClsAbonosVentasCredito> listabono = new List<ClsAbonosVentasCredito>();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == null)
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.BHABILITADO == 1
                                 select new ClsAbonosVentasCredito
                                 {
                                     ID = Abono.ID,
                                     consecutivo = Abono.CONSECUTIVO,
                                     idcliente = Abono.IDCLIENTE,
                                     abono = Abono.ABONO,
                                     fechabono = Abono.FECHABONO

                                 }).ToList();
                }
                else
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.BHABILITADO == 1
                                 && Abono.IDCLIENTE == ClienteParametro
                                 select new ClsAbonosVentasCredito
                                 {
                                     ID = Abono.ID,
                                     consecutivo = Abono.CONSECUTIVO,
                                     idcliente = Abono.IDCLIENTE,
                                     abono = Abono.ABONO,
                                     fechabono = Abono.FECHABONO

                                 }).ToList();


                }
            }
            return PartialView("_TablaAgregarabono", listabono);


        }

        //cargando el combobox Remision
        private void ListaConsecutivo()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.VENTASCREDITO
                         where item.BHABILITADO == 1
                         && item.CONSECUTIVO != "N/A"
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.CONSECUTIVO,
                             Value = item.ID.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione El Consecutivo --", Value = "" });
                ViewBag.ListaConsecutivo = Lista;
            }

        }
        //cargando el combobox Cliente
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
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione El Cliente --", Value = "" });
                ViewBag.ListarCliente = Lista;
            }

        }
        //cargando el combobox Cliente
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
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione Su Usuario --", Value = "" });
                ViewBag.ListarUsuario = Lista;
            }

        }
        //cargando el combobox Cliente
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

        //Metodo para guardar la informacion en la tabla Abonosventascredito
        public string Guardar(ClsAbonosVentasCredito oAbonoCls, int Titulo2)
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
                            if (Titulo2 == -1)
                            {

                                //Instanciamos la clase ABONOSVENTASCREDITO del Modelo
                                ABONOSVENTASCREDITO oAbono = new ABONOSVENTASCREDITO();
                                oAbono.REMISION = oAbonoCls.remision;
                                oAbono.NUIP = oAbonoCls.nuip;
                                oAbono.ABONO = oAbonoCls.abono;
                                oAbono.TIPO = "ABONO";
                                oAbono.FECHABONO = oAbonoCls.fechabono;
                                oAbono.FECHAPAGO = oAbonoCls.fechapago;
                                oAbono.IDUSUARIO = oAbonoCls.idusuario;
                                oAbono.IDCLIENTE = oAbonoCls.idcliente;
                                oAbono.BHABILITADO = 1;
                                oAbono.IDCARRO = oAbonoCls.idcarro;
                                oAbono.CONSECUTIVO = oAbonoCls.Titulo;
                                bd.ABONOSVENTASCREDITO.Add(oAbono);
                                rpta = bd.SaveChanges().ToString();
                                if (rpta == "0") rpta = "";
                                transaccion.Complete();
                            }
                            else
                            {
                                //Editando en la tabla ABONOSVENTASCREDITO
                                ABONOSVENTASCREDITO oAbono = bd.ABONOSVENTASCREDITO.Where(p => p.ID == Titulo2).First();
                                oAbono.REMISION = oAbonoCls.remision;
                                oAbono.NUIP = oAbonoCls.nuip;
                                oAbono.ABONO = oAbonoCls.abono;
                                oAbono.TIPO = oAbonoCls.tipo;
                                oAbono.FECHABONO = oAbonoCls.fechapago;
                                oAbono.FECHAPAGO = oAbonoCls.fechapago;
                                oAbono.IDUSUARIO = oAbonoCls.idusuario;
                                oAbono.IDCLIENTE = oAbonoCls.idcliente;
                                oAbono.BHABILITADO = 1;
                                oAbono.IDCARRO = oAbonoCls.idcarro;
                                oAbono.CONSECUTIVO = oAbonoCls.Titulo;
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


        //Metodo para Recuperara la informacion en funcion de la Remision
        public JsonResult RecuperaInformacion(string id)
        {
            ClsVentasCredito oVentaCreditoCls = new ClsVentasCredito();
            using (var bd = new FESTIVALEntities())
            {
                VENTASCREDITO oVentaCredito = bd.VENTASCREDITO.Where(p => p.CONSECUTIVO == id).First();
                oVentaCreditoCls.Remision = oVentaCredito.REMISION;
                oVentaCreditoCls.Nuip = oVentaCredito.NUIP;
                oVentaCreditoCls.Tipo = oVentaCredito.TIPO;
                oVentaCreditoCls.fechapagocadena = ((DateTime)oVentaCredito.FECHAPAGO).ToString("yyyy-MM-dd");
                oVentaCreditoCls.idcliente = (int)oVentaCredito.IDCLIENTE;
                oVentaCreditoCls.idcarro = (int)oVentaCredito.IDCARRO;
            }

            return Json(oVentaCreditoCls, JsonRequestBehavior.AllowGet);
        }

    }
}