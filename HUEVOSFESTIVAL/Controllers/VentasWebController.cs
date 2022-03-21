using HUEVOSFESTIVAL.Filtros;
using Newtonsoft.Json;
using HUEVOSFESTIVAL.Models;
using HUEVOSFESTIVAL.Models.Home;
using HUEVOSFESTIVAL.Repository;
using HUEVOSFESTIVAL.DAL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;

namespace HUEVOSFESTIVAL.Controllers
{
    [Clientes]
    public class VentasWebController : Controller
    {
        // GET: VentasWeb
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 40, page));
        }

        public string Agregarcookies(string idref, string cantidad,string descripcion,string valventa)
        {
            string rpta = "";
            try
            {
                var referenciaid = ControllerContext.HttpContext.Request.Cookies["referenciaid"];
                var referenciatodo = ControllerContext.HttpContext.Request.Cookies["referenciacantidad"];

                if (referenciaid != null && referenciatodo != null && referenciatodo.Value != ""
                    && referenciaid.Value != "")
                {
                    //se agrega por segunda vez un nuevo idref separado por un {
                    string idcookie = referenciaid.Value + "{" + idref;
                    string datacookie = referenciatodo.Value + "{" + cantidad + "*" + descripcion + "*" + valventa;
                    HttpCookie cookieid = new HttpCookie("referenciaid", idref);
                    HttpCookie cookiedata = new HttpCookie("referenciatodo", datacookie);
                    //agregando las cookies por primera vez
                    ControllerContext.HttpContext.Response.SetCookie(cookieid);
                    ControllerContext.HttpContext.Response.SetCookie(cookiedata);
                }
                else
                {
                    //se crea por primera vez
                    string formatocadena = cantidad + "*" + descripcion + "*" + valventa;
                    HttpCookie cookieid = new HttpCookie("referenciaid", idref);
                    HttpCookie cookiedata = new HttpCookie("referenciatodo", formatocadena);
                    //agregando las cookies por primera vez
                    ControllerContext.HttpContext.Response.SetCookie(cookieid);
                    ControllerContext.HttpContext.Response.SetCookie(cookiedata);

                }
                rpta = "ok";

            }
            catch
            {
                rpta = "";
            }


            return rpta;

        }


            


    }
}