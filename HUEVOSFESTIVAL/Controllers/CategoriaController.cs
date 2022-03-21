using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HUEVOSFESTIVAL.Repository;

namespace HUEVOSFESTIVAL.Controllers
{
    public class CategoriaController : Controller
    {

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();


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
        
        // GET: Categoria
        [Administrador]
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords());
        }

       
        [Administrador]
        public ActionResult EditarCategoria(int idref)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(idref));
        }

        [Administrador]
        [HttpPost]
        public ActionResult EditarCategoria(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Index", "Categoria");
        }


        [Administrador]
        public ActionResult AgregarCategoria()
        {  
            return View();
        }

        [Administrador]
        [HttpPost]
        public ActionResult AgregarCategoria(Tbl_Category tbl)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(tbl);
                return RedirectToAction("Index", "Categoria");
            }
            else
            {
                return RedirectToAction("AgregarCategoria", tbl);
            }
        }


    }
}