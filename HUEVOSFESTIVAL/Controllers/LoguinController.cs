using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HUEVOSFESTIVAL.DAL;
using System.Security.Cryptography;
using System.Text;
using HUEVOSFESTIVAL.ClasesAuxiliares;

namespace HUEVOSFESTIVAL.Controllers
{
    public class LoguinController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CerrarSession()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CerrarSession2()
        {
            Session["Usuario2"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Enter()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Enter(HUEVOSFESTIVAL.DAL.ClsUsuarios usermodel)
        {
          
                string nombreusuario = usermodel.usuario;
                string password = usermodel.contraseña;
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(password);
                byte[] byteContaCifrado = sha.ComputeHash(byteContra);
                string cadenaContraCifrada = BitConverter.ToString(byteContaCifrado).Replace("-", "");
                using (FESTIVALEntities bd = new FESTIVALEntities())
                {
                    var Userdetails = bd.USUARIOS.Where(p => p.USUARIO == nombreusuario
                    && p.CONTRASEÑA == cadenaContraCifrada).FirstOrDefault();
                    if (Userdetails == null)
                    {
                        ////Redireccionar al Login de nuevo
                        return View("Enter", Userdetails);

                    }
                    else
                    {
                        if (Userdetails.TIPOUSUARIO == "E")
                        {
                            ////Agregando el nombre usuario a la Session que se abre
                            Session["Usuario"] = bd.USUARIOS.Where(p => p.USUARIO == nombreusuario
                            && p.CONTRASEÑA == cadenaContraCifrada);
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (Userdetails.TIPOUSUARIO == "A")
                        {
                            ////Agregando el nombre usuario a la Session que se abre
                            Session["Administrador"] = bd.USUARIOS.Where(p => p.USUARIO == nombreusuario
                            && p.CONTRASEÑA == cadenaContraCifrada);
                            return RedirectToAction("Index", "Referencias");
                        }
                        else
                        {
                            Session["Cliente"] = bd.USUARIOS.Where(p => p.USUARIO == nombreusuario
                           && p.CONTRASEÑA == cadenaContraCifrada);
                            return RedirectToAction("Index", "VentasWeb");
                        }

                    }
                }
            
        }

           




        public string RecuperarContra(string IIDTIPO, string correo, string telefonoCelular)
        {
            string mensaje = "";
            using (var bd = new FESTIVALEntities())
            {
                int cantidad = 0;
                int iidcliente;
                if (IIDTIPO == "C")
                {
                    //Existe un cliente con esa informacion
                    cantidad = bd.CLIENTES.Where(p => p.EMAIL == correo && p.TELEFONO == telefonoCelular).Count();
                }
                if (cantidad == 0) mensaje = "No existe una persona registrada con esa informacion";
                else
                {
                    iidcliente = bd.CLIENTES.Where(p => p.EMAIL == correo && p.TELEFONO == telefonoCelular).First().IDCLIENTE;
                    //Verificar si existe el usuario
                    int nveces = bd.USUARIOS.Where(p => p.IID == iidcliente && p.TIPOUSUARIO == "C").Count();
                    if (nveces == 0)
                    {
                        mensaje = "No tiene usuario";
                    }
                    else
                    {
                        //Ontener su Id
                        USUARIOS ousuario = bd.USUARIOS.Where(p => p.IID == iidcliente && p.TIPOUSUARIO == "C").First();
                        //Modificar su clave
                        Random ra = new Random();
                        int n1 = ra.Next(0, 9);
                        int n2 = ra.Next(0, 9);
                        int n3 = ra.Next(0, 9);
                        int n4 = ra.Next(0, 9);
                        string nuevaContra = n1.ToString() + n2 + n3 + n4;
                        //Cifrar clave

                        SHA256Managed sha = new SHA256Managed();
                        byte[] byteContra = Encoding.Default.GetBytes(nuevaContra);
                        byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                        string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");

                        ousuario.CONTRASEÑA = cadenaContraCifrada;
                        mensaje = bd.SaveChanges().ToString();
                        CORREO.enviarCorreo(correo, "Recuperar Clave", "Se reseteo su clave , ahora su clave es :" + nuevaContra, "");
                    }
                }
            }
            return mensaje;
        }





    }
}