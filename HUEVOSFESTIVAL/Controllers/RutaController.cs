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
    public class RutaController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        [Administrador]
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepositoryInstance<RUTA>().GetAllRecords());
        }


        [Administrador]
        public ActionResult AgregarRuta()
        {
            return View();
        }

        [Administrador]
        [HttpPost]
        public ActionResult AgregarRuta(RUTA tbl)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstance<RUTA>().Add(tbl);
                return RedirectToAction("Index", "Ruta");
            }
            else
            {
                return RedirectToAction("AgregarRuta", tbl);
            }

        }

        [Administrador]
        public ActionResult EditarRuta(int idruta)
        {
            return View(_unitOfWork.GetRepositoryInstance<RUTA>().GetFirstorDefault(idruta));
        }


        [Administrador]
        [HttpPost]
        public ActionResult EditarRuta(RUTA tbl)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstance<RUTA>().Update(tbl);
                return RedirectToAction("Index", "Ruta");
            }
            else
            {
                return RedirectToAction("EditarRuta", tbl);
            }
        }


    }
}