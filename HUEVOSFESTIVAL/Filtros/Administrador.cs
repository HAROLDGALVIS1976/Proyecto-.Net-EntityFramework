using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Filtros
{
    public class Administrador : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Si session es nulo , entonces retorne al Login
            var usuario2 = HttpContext.Current.Session["Administrador"];

            if (usuario2 == null)
            {                                           //Controlado / Vista
                filterContext.Result = new RedirectResult("~/Loguin/Enter");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}