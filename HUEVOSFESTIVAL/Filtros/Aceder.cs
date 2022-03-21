using HUEVOSFESTIVAL.Controllers;
using HUEVOSFESTIVAL.DAL;
using HUEVOSFESTIVAL.Models;
using HUEVOSFESTIVAL.Repository;
using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;



namespace HUEVOSFESTIVAL.Filtros
{
    public class Aceder : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Si session es nulo , entonces retorne al Login
            var usuario = HttpContext.Current.Session["Usuario"];
           
            if (usuario == null)
            {                                           //Controlado / Vista
                filterContext.Result = new RedirectResult("~/Loguin/Enter");
            }
            base.OnActionExecuting(filterContext);
        }



    }

}



   





   