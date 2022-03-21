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
using System.Security.Cryptography;
using System.Text;

namespace HUEVOSFESTIVAL.Controllers
{
    public class UsuariosController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // GET: Usuarios
        [Administrador]
        public ActionResult Index()
        {
            return View(_unitOfWork.GetRepositoryInstance<USUARIOS>().GetAllRecords());
        }

        private void ListarTipoUsuario()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.TIPOUSUARIOREGISTRO
                         where item.TIPOUSUARIO != null
                         select new SelectListItem
                         {
                             /* propiedad Texto para visualizar contenido en DropDownList en la vista Clientes*/
                             Text = item.TIPOUSUARIO,
                             Value = item.TIPOUSUARIO.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione el TipoUsuario --", Value = "" });
                ViewBag.ListarTipoUsuario = Lista;
            }

        }

        [Administrador]
        public ActionResult AgregarUsuarios()
        {
            ListarTipoUsuario();
            return View ();
        }


        [Administrador]
        [HttpPost]
        public ActionResult AgregarUsuarios(USUARIOS tbl)
        {
            if (ModelState.IsValid)
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(tbl.CONTRASEÑA);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                tbl.CONTRASEÑA = cadenaContraCifrada;
                tbl.BHABILITADO = 1;
                _unitOfWork.GetRepositoryInstance<USUARIOS>().Add(tbl);
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return RedirectToAction("AgregarUsuarios", tbl);
            }
        }


        [Administrador]
        public ActionResult EditarUsuario(int idusuario)
        {
            ListarTipoUsuario();
            return View(_unitOfWork.GetRepositoryInstance<USUARIOS>().GetFirstorDefault(idusuario));
        }


        [Administrador]
        [HttpPost]
        public ActionResult EditarUsuario(USUARIOS tbl)
        {
            if (ModelState.IsValid)
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(tbl.CONTRASEÑA);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                tbl.CONTRASEÑA = cadenaContraCifrada;
                tbl.BHABILITADO = 1;
                _unitOfWork.GetRepositoryInstance<USUARIOS>().Update(tbl);
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return RedirectToAction("EditarUsuario", tbl);
            }
        }

    }
}