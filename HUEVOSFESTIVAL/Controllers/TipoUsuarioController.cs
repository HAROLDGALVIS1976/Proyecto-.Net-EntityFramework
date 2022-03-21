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
    public class TipoUsuarioController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // GET: TipoUsuario
        [Administrador]
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepositoryInstance<TIPOUSUARIOREGISTRO>().GetAllRecords());
        }

        [Administrador]
        public ActionResult AgregarTipoUsuario()
        {
            return View();
        }

        [Administrador]
        [HttpPost]
        public ActionResult AgregarTipoUsuario(TIPOUSUARIOREGISTRO tbl)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GetRepositoryInstance<TIPOUSUARIOREGISTRO>().Add(tbl);
                return RedirectToAction("Index", "TipoUsuario");
            }
            else
            {
                return RedirectToAction("AgregarTipoUsuario", tbl);
            }

        }

        

    }
}