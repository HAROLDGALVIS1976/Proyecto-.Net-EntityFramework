using HUEVOSFESTIVAL.DAL;
//Libreria para realizar el bloqueo de ingreso a rutas web sin loguearse
using HUEVOSFESTIVAL.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using System.Web;
//Librerias Activas despues de Agregar a Referenca el itextsharp.dll
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Drawing;
//Librerias Activas despues de descargar Paquete Nuguet EP PLUS
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace HUEVOSFESTIVAL.Controllers
{
    public class Referencias2Controller : Controller
    {
       


        public FileResult generarPDF()
        {
            Document doc = new Document();
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("Huevos Festival");
                Paragraph subtitle = new Paragraph("Referencias");
                
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                subtitle.Alignment = Element.ALIGN_CENTER;
                doc.Add(subtitle);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);

                //Columnas (Tabla)
                PdfPTable table = new PdfPTable(3);
                //anchos a las columnas
                float[] values = new float[3] { 5, 20, 5 };
                //asignado esos anchos a la tabla
                table.SetWidths(values);


                //Creando celdas(Poniendo contenido)-color-alineado
                //el contenido al centro
                PdfPCell celda1 = new PdfPCell(new Phrase("Id "));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Phrase("Descripcion"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("Catidad"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(celda3);

                List<ClsReferencias> lista = (List<ClsReferencias>)Session["lista"];
                int nregistros = lista.Count;
                for (int i = 0; i < nregistros; i++)
                {
                    table.AddCell(lista[i].idref.ToString());
                    table.AddCell(lista[i].descripcion);
                    table.AddCell(lista[i].cant.ToString());
                }

                doc.Add(table);
                doc.Close();

                buffer = ms.ToArray();
            }


            return File(buffer, "application/pdf");
        }

        public FileResult generarExcel()
        {
            byte[] buffer;

            using (MemoryStream ms = new MemoryStream())
            {
                //Todo el documento excel
                ExcelPackage ep = new ExcelPackage();
                //Crear un hoja
                ep.Workbook.Worksheets.Add("Reporte");

                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                //Ponemos nombre de las columnas
                ew.Cells[1, 1].Value = "Id";
                ew.Cells[1, 2].Value = "Descripcion";
                ew.Cells[1, 3].Value = "Cant";
                ew.Column(1).Width = 20;
                ew.Column(2).Width = 40;
                ew.Column(3).Width = 180;
                using (var range = ew.Cells[1, 1, 1, 3])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                }
                List<ClsReferencias> lista = (List<ClsReferencias>)Session["lista"];
                int nregistros = lista.Count;
                //Pendiente
                for (int i = 0; i < nregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = lista[i].idref;
                    ew.Cells[i + 2, 2].Value = lista[i].descripcion;
                    ew.Cells[i + 2, 3].Value = lista[i].cant;

                }
                //Pendiente
                ep.SaveAs(ms);
                buffer = ms.ToArray();

            }

            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        [Administrador]
        public ActionResult Index(ClsReferencias oClsReferencias)
        {
            int idref = oClsReferencias.idref;
            /* Creacion de la ListaVentasCredito para mostrar en <tabla> en la vista Abonos)*/
            List<ClsReferencias> Listareferencias = null;
            //Lista para llenar el DropDownList Remision
            ListarCategoria();
            ListarRef();
            using (var bd = new FESTIVALEntities())
            {
                /*si el idref == 0 cuando no hay seleccion en el helper  @Html.DropDownList de la vista Index ==>*/
                if (idref == 0)
                {
                    Listareferencias = (from Refrencias in bd.REFERENCIAS
                                        where Refrencias.CANT != 0

                                        select new ClsReferencias
                                        {
                                            idref = Refrencias.IDREF,
                                            descripcion = Refrencias.DESCRIPCION,
                                            cant = Refrencias.CANT

                                        }).ToList();
                    Session["lista"] = Listareferencias;
                }
                else
                {
                    Listareferencias = (from Refrencias in bd.REFERENCIAS
                                        where Refrencias.CANT != 0
                                        && Refrencias.IDREF == idref
                                        select new ClsReferencias
                                        {
                                            idref = Refrencias.IDREF,
                                            descripcion = Refrencias.DESCRIPCION,
                                            cant = Refrencias.CANT

                                        }).ToList();
                    Session["lista"] = Listareferencias;

                }
            }
            return View(Listareferencias);
        }

        //cargando el combobox Categoria de Ventas
        private void ListarRef()
        {
            List<SelectListItem> Lista = null;
            using (var bd = new FESTIVALEntities())
            {
                Lista = (from item in bd.REFERENCIAS
                         where item.CANT != 0
                         select new SelectListItem
                         {
                             
                             Text = item.DESCRIPCION,
                             Value = item.IDREF.ToString()
                         }).ToList();
                Lista.Insert(0, new SelectListItem { Text = "-- Seleccione la Referencia --", Value = "" });
                ViewBag.ListarRef = Lista;
            }

        }

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

        //Filtro en funcio del Parametro lugarRutaParametro
        public ActionResult Filtrar(int? Parametro)
        {
            List<ClsReferencias> Listareferencias = new List<ClsReferencias>();
            using (var bd = new FESTIVALEntities())
            {
                if (Parametro == null)
                {
                    Listareferencias = (from Refrencias in bd.REFERENCIAS
                                        where Refrencias.CANT != 0

                                        select new ClsReferencias
                                        {
                                            idref = Refrencias.IDREF,
                                            descripcion = Refrencias.DESCRIPCION,
                                            cant = Refrencias.CANT

                                        }).ToList();
                    Session["lista"] = Listareferencias;
                }
                else
                {
                    Listareferencias = (from Refrencias in bd.REFERENCIAS
                                        where Refrencias.CANT != 0
                                        && Refrencias.IDREF == Parametro
                                        select new ClsReferencias
                                        {
                                            idref = Refrencias.IDREF,
                                            descripcion = Refrencias.DESCRIPCION,
                                            cant = Refrencias.CANT

                                        }).ToList();
                    Session["lista"] = Listareferencias;

                }


            }
            return PartialView("_TablaReferencias", Listareferencias);

        }

        //Metodo para guardar o Editar la informacion en la tabla 
        public string Guardar(ClsReferencias oClsReferencias, int titulo, HttpPostedFileBase imagen)
        {

            string rpta = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = (from state in ModelState.Values
                                 from error in state.Errors
                                 select error.ErrorMessage).ToList();
                    rpta += "<ul class='list-group'>";
                    foreach (var item in query)
                    {
                        rpta += "<li class='list-group-item'>" + item + "</li>";
                    }
                    rpta += "</ul>";
                }
                else
                {
                    using (var bd = new FESTIVALEntities())
                    {
                        
                       

                       
                            string pic = null;
                            if (imagen != null)
                            {
                                pic = System.IO.Path.GetFileName(imagen.FileName);
                                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                                // file is uploaded
                                imagen.SaveAs(path);
                            }


                            if (titulo == -1)
                            {
                                //Instanciamos la clase VENTA_PRODWEB del Modelo
                                REFERENCIAS oReferencia = new REFERENCIAS();
                                oReferencia.REF = oClsReferencias.Ref;
                                oReferencia.DESCRIPCION = oClsReferencias.descripcion;
                                oReferencia.CANT = oClsReferencias.cant;
                                oReferencia.VALCOSTO_UNI = oClsReferencias.valcosto_uni;
                                oReferencia.VALUNI_VENTA = oClsReferencias.valuni_venta;
                                oReferencia.CANT_MINIMA = oClsReferencias.cant_minima;
                                oReferencia.VALCOSTO_TOTAL = oClsReferencias.valcosto_total;
                                oReferencia.VALVENTA_TOTAL = oClsReferencias.valventa_total;
                                oReferencia.IMAGEN = pic;
                                oReferencia.UNI_MEDIDA = oClsReferencias.uni_medida;
                                oReferencia.CategoryId = oClsReferencias.categoryid;
                                oReferencia.BHABILITADO = 0;
                                oReferencia.VALUNI_VENTA = oClsReferencias.valmin_venta;
                                oReferencia.PORCENTAJE = oClsReferencias.porcentaje;
                               
                                bd.REFERENCIAS.Add(oReferencia);
                                rpta = bd.SaveChanges().ToString();
                                if (rpta == "0") rpta = "";
                                
                            }
                            else
                            {
                                REFERENCIAS oReferencia = bd.REFERENCIAS.Where(p => p.IDREF == titulo).First();
                                oReferencia.REF = oClsReferencias.Ref;
                                oReferencia.DESCRIPCION = oClsReferencias.descripcion;
                                oReferencia.CANT = oClsReferencias.cant;
                                oReferencia.VALCOSTO_UNI = oClsReferencias.valcosto_uni;
                                oReferencia.VALUNI_VENTA = oClsReferencias.valuni_venta;
                                oReferencia.CANT_MINIMA = oClsReferencias.cant_minima;
                                oReferencia.VALCOSTO_TOTAL = oClsReferencias.valcosto_total;
                                oReferencia.VALVENTA_TOTAL = oClsReferencias.valventa_total;
                                oReferencia.IMAGEN = pic;
                                oReferencia.UNI_MEDIDA = oClsReferencias.uni_medida;
                                oReferencia.CategoryId = oClsReferencias.categoryid;
                                oReferencia.BHABILITADO = 0;
                                oReferencia.VALUNI_VENTA = oClsReferencias.valmin_venta;
                                oReferencia.PORCENTAJE = oClsReferencias.porcentaje;

                                rpta = bd.SaveChanges().ToString();
                              
                            }

                        
                    }
                }
            }
            catch (Exception)
            {
                rpta = "";
            }

            return rpta;


        }



        public JsonResult RecuperarInformacion(int idref)
        {
            ClsReferencias oReferenciacls = new ClsReferencias();
            using (var bd = new FESTIVALEntities())
            {
                REFERENCIAS oReferencia = bd.REFERENCIAS.Where(p => p.IDREF == idref).First();
                oReferenciacls.Ref = oReferencia.REF;
                oReferenciacls.descripcion = oReferencia.DESCRIPCION;
                oReferenciacls.cant = (double)oReferencia.CANT;
                oReferenciacls.valcosto_uni = (double)oReferencia.VALCOSTO_UNI;
                oReferenciacls.valuni_venta = (double)oReferencia.VALUNI_VENTA;
                oReferenciacls.cant_minima = (double)oReferencia.CANT_MINIMA;
                oReferenciacls.valcosto_total = (double)oReferencia.VALCOSTO_TOTAL;
                oReferenciacls.valventa_total = (double)oReferencia.VALVENTA_TOTAL;             
                oReferenciacls.uni_medida = oReferencia.UNI_MEDIDA;
                oReferenciacls.categoryid = (int)oReferencia.CategoryId;
                oReferenciacls.valmin_venta = (double)oReferencia.VALMIN_VENTA;
                oReferenciacls.porcentaje = (double)oReferencia.PORCENTAJE;

            }

            return Json(oReferenciacls, JsonRequestBehavior.AllowGet);
        }






    }
}