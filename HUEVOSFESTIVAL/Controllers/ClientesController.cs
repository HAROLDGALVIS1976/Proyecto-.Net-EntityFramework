using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Controllers
{
    //Tag para bloque sin iniciar session
   [Aceder]
    public class ClientesController : Controller
    {
        // GET: Clientes

        public ActionResult Index(ClsClientes oClientesCls)
        {
            int RutaParametro = oClientesCls.idruta;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsClientes> ListaClientes = null;
            //Procedimiento para listar los clientes
            ListarRutas();
            using (var bd = new FESTIVALEntities())
            {
                if (RutaParametro == 0)
                {
                    ListaClientes = (from clientes in bd.CLIENTES
                                     where clientes.BHABILITADO == 1
                                     orderby clientes.CLIENTE ascending
                                     select new ClsClientes
                                     {
                                         idcliente = clientes.IDCLIENTE,
                                         cliente = clientes.CLIENTE,
                                         idruta = (int)clientes.IDRUTA

                                     }).ToList();
                }
                else
                {
                    ListaClientes = (from clientes in bd.CLIENTES
                                     where clientes.BHABILITADO == 1
                                     && clientes.IDRUTA == RutaParametro
                                     orderby clientes.CLIENTE ascending
                                     select new ClsClientes
                                     {
                                         idcliente = clientes.IDCLIENTE,
                                         cliente = clientes.CLIENTE,
                                         idruta = (int)clientes.IDRUTA

                                     }).ToList();


                }
            }
            return View(ListaClientes);
        }


        //cargando el combobox Rutas de Ventas
        private void ListarRutas()
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
                ViewBag.ListarRutas = Lista;
            }

        }

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? RutaParametro)
        {
            List<ClsClientes> ListaClientes = new List<ClsClientes>();
            using (var bd = new FESTIVALEntities())
            {
                if (RutaParametro == null)
                {
                    ListaClientes = (from clientes in bd.CLIENTES
                                     where clientes.BHABILITADO == 1
                                     orderby clientes.CLIENTE ascending
                                     select new ClsClientes
                                     {
                                         idcliente = clientes.IDCLIENTE,
                                         cliente = clientes.CLIENTE,
                                         idruta = (int)clientes.IDRUTA

                                     }).ToList();
                }
                else
                {
                    ListaClientes = (from clientes in bd.CLIENTES
                                     where clientes.BHABILITADO == 1
                                     && clientes.IDRUTA == RutaParametro
                                     orderby clientes.CLIENTE ascending
                                     select new ClsClientes
                                     {
                                         idcliente = clientes.IDCLIENTE,
                                         cliente = clientes.CLIENTE,
                                         idruta = (int)clientes.IDRUTA

                                     }).ToList();
                }


            }
            return PartialView("_TablaClientes", ListaClientes);

        }

        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar(ClsClientes oClienteCls, int titulo)
        {
            string mensaje = "";

            //Error
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();
                mensaje += "<ul class='list-group'>";
                foreach (var item in query)
                {
                    mensaje += "<li class='list-group-item'>" + item + "</li>";
                }
                mensaje += "</ul>";
            }
            else
            {
                string nombrecliente = oClienteCls.cliente;
                string nuipcliente = oClienteCls.nuip;
                using (var bd = new FESTIVALEntities())
                {
                    int numeroVeces = bd.CLIENTES.Where(p => p.CLIENTE == nombrecliente
                     && p.NUIP == nuipcliente).Count();
                    mensaje = numeroVeces.ToString();
                    if (mensaje == "0" && titulo == -1)
                    {
                        //Guardar
                        //Instanciamos la clase CLIENTES del Modelo
                        CLIENTES oCliente = new CLIENTES();
                        oCliente.CLIENTE = oClienteCls.cliente;
                        oCliente.NUIP = oClienteCls.nuip;
                        oCliente.DIRECCION = oClienteCls.direccion;
                        oCliente.TELEFONO = oClienteCls.telefono;
                        oCliente.CIUDAD = oClienteCls.ciudad;
                        oCliente.EMAIL = oClienteCls.email;
                        oCliente.IDRUTA = oClienteCls.idruta;
                        oCliente.BHABILITADO = 1;
                        oCliente.TIPOUSUARIO = "C";
                        bd.CLIENTES.Add(oCliente);
                        mensaje = bd.SaveChanges().ToString();
                        if (mensaje == "0") mensaje = "";
                    }
                    else if (mensaje == "1" && titulo >= 1)
                    {
                        //Modificar
                        CLIENTES oCliente = bd.CLIENTES.Where(p => p.IDCLIENTE == titulo).First();
                        oCliente.CLIENTE = oClienteCls.cliente;
                        oCliente.NUIP = oClienteCls.nuip;
                        oCliente.DIRECCION = oClienteCls.direccion;
                        oCliente.TELEFONO = oClienteCls.telefono;
                        oCliente.CIUDAD = oClienteCls.ciudad;
                        oCliente.EMAIL = oClienteCls.email;
                        oCliente.IDRUTA = oClienteCls.idruta;
                        oCliente.TIPOUSUARIO = "C";
                        mensaje = bd.SaveChanges().ToString();

                    }
                    else
                    {
                        mensaje = "El Cliente y el Documento ya Existen en la Base de Datos, Ingrese otro Cliente o Documento";

                    }
                }
            }

            return mensaje;
        }

        //Recuperar informacion en funcion del id
        public JsonResult RecuperaInformacion(int idref)
        {
            ClsClientes oClienteCls = new ClsClientes();
            using (var bd = new FESTIVALEntities())
            {
                CLIENTES oCliente = bd.CLIENTES.Where(p => p.IDCLIENTE == idref).First();
                oClienteCls.cliente = oCliente.CLIENTE;
                oClienteCls.nuip = oCliente.NUIP;
                oClienteCls.direccion = oCliente.DIRECCION;
                oClienteCls.telefono = oCliente.TELEFONO;
                oClienteCls.ciudad = oCliente.CIUDAD;
                oClienteCls.email = oCliente.EMAIL;
                oClienteCls.idruta = (int)oCliente.IDRUTA;
            }

            return Json(oClienteCls, JsonRequestBehavior.AllowGet);
        }




    }
}