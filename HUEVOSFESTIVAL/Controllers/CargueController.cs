using HUEVOSFESTIVAL.DAL;
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Controllers
{
    //Tag para bloquear vistas sin iniciar session
    [Aceder]

    public class CargueController : Controller
    {
        // GET: Cargue

        public ActionResult Index(ClsCargue oCargueCls)
        {
            int idcargue = oCargueCls.idcarga;
            List<ClsCargue> listaCargue = null;
            ListarCarga();
            using (var bd = new FESTIVALEntities())
            {
                /*si el iidsexo == 0 cuando no hay seleccion en el helper  @Html.DropDownList de la vista Index ==>*/
                if (idcargue == 0)
                {
                    listaCargue = (from cargue in bd.CARGUE
                                   where cargue.BHABILITADO == 1
                                   select new ClsCargue
                                   {
                                       idcarga = cargue.IDCARGA,
                                       extr = cargue.EXTR,
                                       aar = cargue.AAR,
                                       ar = cargue.AR,
                                       br = cargue.BR,
                                       extb = cargue.EXTB,
                                       aab = cargue.AAB,
                                       ab = cargue.AB,
                                       bb = cargue.BB

                                   }).ToList();
                }
                else
                {
                    listaCargue = (from cargue in bd.CARGUE
                                   where cargue.BHABILITADO == 1
                                   && cargue.IDCARGA == idcargue
                                   select new ClsCargue
                                   {
                                       idcarga = cargue.IDCARGA,
                                       extr = cargue.EXTR,
                                       aar = cargue.AAR,
                                       ar = cargue.AR,
                                       br = cargue.BR,
                                       extb = cargue.EXTB,
                                       aab = cargue.AAB,
                                       ab = cargue.AB,
                                       bb = cargue.BB
                                   }).ToList();
                }
            }
            return View(listaCargue);
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
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione el Id --", Value = "" });
                ViewBag.ListarCarga = Lista;
            }

        }

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? Parametro)
        {
            List<ClsCargue> lista = new List<ClsCargue>();
            using (var bd = new FESTIVALEntities())
            {
                /*si el iidsexo == 0 cuando no hay seleccion en el helper  @Html.DropDownList de la vista Index ==>*/
                if (Parametro == 0)
                {
                    lista = (from cargue in bd.CARGUE
                             where cargue.BHABILITADO == 1
                             select new ClsCargue
                             {
                                 idcarga = cargue.IDCARGA,
                                 extr = cargue.EXTR,
                                 aar = cargue.AAR,
                                 ar = cargue.AR,
                                 br = cargue.BR,
                                 extb = cargue.EXTB,
                                 aab = cargue.AAB,
                                 ab = cargue.AB,
                                 bb = cargue.BB

                             }).ToList();
                }
                else
                {
                    lista = (from cargue in bd.CARGUE
                             where cargue.BHABILITADO == 1
                             && cargue.IDCARGA == Parametro
                             select new ClsCargue
                             {
                                 idcarga = cargue.IDCARGA,
                                 extr = cargue.EXTR,
                                 aar = cargue.AAR,
                                 ar = cargue.AR,
                                 br = cargue.BR,
                                 extb = cargue.EXTB,
                                 aab = cargue.AAB,
                                 ab = cargue.AB,
                                 bb = cargue.BB

                             }).ToList();
                }
            }
            return PartialView("_TablaCargue", lista);
        }

        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar(ClsCargue oCargueCls, int titulo)
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
                            CARGUE2 oCargue = new CARGUE2();
                            oCargue.EXTR = oCargueCls.extr;
                            oCargue.AAR = oCargueCls.aar;
                            oCargue.AR = oCargueCls.ar;
                            oCargue.BR = oCargueCls.br;
                            oCargue.EXTB = oCargueCls.extb;
                            oCargue.AAB = oCargueCls.aab;
                            oCargue.AB = oCargueCls.ab;
                            oCargue.BB = oCargueCls.bb;
                            oCargue.IDUSUARIO = oCargueCls.idusuario;
                            oCargue.IDCARRO = oCargueCls.idcarro;
                            oCargue.IDCARGA = oCargueCls.idcarga;
                            oCargue.FECHA = oCargueCls.fecha;
                            oCargue.BHABILITADO = 1;
                            bd.CARGUE2.Add(oCargue);
                            rpta = bd.SaveChanges().ToString();
                            if (rpta == "0") rpta = "";
                        }
                        else
                        {
                            //Editando en la tabla VENTASCREDITO
                            CARGUE2 oCargue = bd.CARGUE2.Where(p => p.IDCARGA == titulo).First();
                            oCargue.EXTR = oCargueCls.extr;
                            oCargue.AAR = oCargueCls.aar;
                            oCargue.AR = oCargueCls.ar;
                            oCargue.BR = oCargueCls.br;
                            oCargue.EXTB = oCargueCls.extb;
                            oCargue.AAB = oCargueCls.aab;
                            oCargue.AB = oCargueCls.ab;
                            oCargue.BB = oCargueCls.bb;
                            oCargue.IDUSUARIO = oCargueCls.idusuario;
                            oCargue.IDCARRO = oCargueCls.idcarro;
                            oCargue.IDCARGA = oCargueCls.idcarga;
                            oCargue.FECHA = oCargueCls.fecha;
                            oCargue.BHABILITADO = 1;                           
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


        public JsonResult RecuperaInformacion(int id)
            {
                ClsCargue oCargueCls = new ClsCargue();
                using (var bd = new FESTIVALEntities())
                {
                    CARGUE2 oCargue = bd.CARGUE2.Where(p => p.IDCARGA == id).First();
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
                    oCargueCls.idcarga = (int)oCargue.IDCARGA;
                    oCargueCls.Fechacadena = ((DateTime)oCargue.FECHA).ToString("yyyy-MM-dd");
                }

                return Json(oCargueCls, JsonRequestBehavior.AllowGet);
            }




       


    }
}