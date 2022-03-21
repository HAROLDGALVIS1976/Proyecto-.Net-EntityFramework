using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Controllers
{
    public class ConsultarAbonosController : Controller
    {
        //Tag para bloque sin iniciar session
        [Aceder]
        // GET: ConsultarAbonos
    
        public ActionResult Index(ClsAbonosVentasCredito oAgregarabonoCls)
        {
            ListarCliente();
            int Cliente = oAgregarabonoCls.idcliente;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsAbonosVentasCredito> listabono = null;
            //Lista para llenar el DropDownList Remision
            using (var bd = new FESTIVALEntities())
            {
                /*si el idref == 0 cuando no hay seleccion en el helper  @Html.DropDownList de la vista Index ==>*/
                if (Cliente == 0)
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.ABONO != 0
                                 && Abono.CONSECUTIVO != "N/A"
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
                                 where Abono.ABONO != 0
                                 && Abono.CONSECUTIVO != "N/A"
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


        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? ClienteParametro)
        {
            List<ClsAbonosVentasCredito> listabono = new List<ClsAbonosVentasCredito>();
            using (var bd = new FESTIVALEntities())
            {
                if (ClienteParametro == null)
                {
                    listabono = (from Abono in bd.ABONOSVENTASCREDITO
                                 where Abono.ABONO != 0
                                 && Abono.CONSECUTIVO != "N/A"
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
                                 where Abono.ABONO != 0
                                 && Abono.CONSECUTIVO != "N/A"
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
            return PartialView("_TablaAbonos", listabono);


        }





    }
}