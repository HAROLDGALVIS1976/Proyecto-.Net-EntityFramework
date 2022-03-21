using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using System.Web;
using HUEVOSFESTIVAL.Repository;

namespace HUEVOSFESTIVAL.Controllers
{
    public class ReferenciasController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();


        [Administrador]
        // GET: Refrencias
        public ActionResult Index()
        {
           return View(_unitOfWork.GetRepositoryInstance<REFERENCIAS>().GetProduct());
           
        }

        //Procedimiento para listar las Categorias
        //public List<SelectListItem> GetCategory()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
        //    foreach (var item in cat)
        //    {
        //        list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.NombreCategoria });
        //    }
        //    return list;
        //}

        //cargando el combobox Rutas de Ventas

        private void ListarCategoria()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.Tbl_Category
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.NombreCategoria,
                             Value = item.CategoryId.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione la Categoria --", Value = "" });
                ViewBag.ListarCategoria = Lista;
            }

        }

        [Administrador]
        public ActionResult EditarRefrencias(int idref)
        {
           
            //ViewBag.CategoryList = GetCategory();
            ListarCategoria();
            return View(_unitOfWork.GetRepositoryInstance<REFERENCIAS>().GetFirstorDefault(idref));
        }

        [Administrador]
        [HttpPost]
        public ActionResult EditarRefrencias(REFERENCIAS tbl, HttpPostedFileBase IMAGEN)
        {
            if (ModelState.IsValid)
            {
                string pic = null;
                if (IMAGEN != null)
                {
                    pic = System.IO.Path.GetFileName(IMAGEN.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                    // file is uploaded
                    IMAGEN.SaveAs(path);
                }
                tbl.IMAGEN = IMAGEN != null ? pic : tbl.IMAGEN;
                //tbl.CANT= (int)tbl.CANT;
                //tbl.VALCOSTO_UNI = (int)tbl.VALCOSTO_UNI;
                //tbl.VALUNI_VENTA = (int)tbl.VALUNI_VENTA;
                //tbl.VALUNI_VENTA = (int)tbl.VALMIN_VENTA;
                //tbl.VALCOSTO_TOTAL = (int)tbl.VALCOSTO_TOTAL;
                //tbl.VALVENTA_TOTAL = (int)tbl.VALVENTA_TOTAL;
                tbl.BHABILITADO = 1;
                _unitOfWork.GetRepositoryInstance<REFERENCIAS>().Update(tbl);
                return RedirectToAction("Index", "Referencias");
            }
            else
            {
                return RedirectToAction("EditarRefrencias", tbl);
            }


        }


        [Administrador]
        public ActionResult AgregarReferencias()
        {

            // ViewBag.CategoryList = GetCategory();
            ListarCategoria();
            return View();
        }

        [Administrador]
        [HttpPost]
        public ActionResult AgregarReferencias(REFERENCIAS tbl, HttpPostedFileBase IMAGEN)
        {
            if (ModelState.IsValid)
            {
                string pic = null;
                if (IMAGEN != null)
                {
                    pic = System.IO.Path.GetFileName(IMAGEN.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                    // file is uploaded
                    IMAGEN.SaveAs(path);
                }
                tbl.IMAGEN = pic;
                tbl.BHABILITADO = 1;
                _unitOfWork.GetRepositoryInstance<REFERENCIAS>().Add(tbl);
                return RedirectToAction("Index", "Referencias");
            }
            else
            {
                return RedirectToAction("AgregarReferencias", tbl);
            }


        }
    }
}