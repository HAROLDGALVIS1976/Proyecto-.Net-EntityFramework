using Newtonsoft.Json;
using HUEVOSFESTIVAL.Models;
using HUEVOSFESTIVAL.Models.Home;
using HUEVOSFESTIVAL.Repository;
using HUEVOSFESTIVAL.DAL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HUEVOSFESTIVAL.Controllers
{
    public class HomeController : Controller
    {
        FESTIVALEntities ctx = new FESTIVALEntities();

        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 40, page));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Quienes Somos?.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Quienes Somos.";

            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }


        public ActionResult AddToCart(int? productId)
        {
            try

            {
                  if (Session["cart"] == null)
                    {
                        List<Item> cart = new List<Item>();
                        var product = ctx.REFERENCIAS.Find(productId);
                        
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        Session["cart"] = cart;
                    }
                    else
                    {
                        List<Item> cart = (List<Item>)Session["cart"];
                        var product = ctx.REFERENCIAS.Find(productId);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        Session["cart"] = cart;
                    }

                    return Redirect("Index");
                
            }
            catch (Exception)
            {
                return Redirect("Index");
            }


        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.REFERENCIAS.Find(productId);

                foreach (var item in cart)
                {
                    if (item.Product.IDREF == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty + 1
                            });

                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkoutdetails");

        }


        //Action Link para Remover Item del Carrito
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.IDREF == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }


        public ActionResult Sweet(string Prod, int Cant, int Total)
        {

            Response.Redirect("https://api.whatsapp.com/send?phone=+573113395819&text=!Agregue%20Su%20Nombre%20y%20Direccion%20por%20Favor%20para%20Enviarle%20los  Productos: " + Prod + "  ---Cantidad: " + Cant + "  ---Total:  " + Total + "");
            return View();

        }
    }
}