using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsReferencias
    {

        public int idref { get; set; }
        [Required]
        [Display(Name = "Referencia")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string Ref { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string descripcion { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double cant { get; set; }
        [Required]
        [Display(Name = "ValCosto-Unidad")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double valcosto_uni { get; set; }
        [Required]
        [Display(Name = "ValVenta-Unidad")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double valuni_venta { get; set; }
        [Required]
        [Display(Name = "Cantidad-Minima")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double cant_minima { get; set; }
        [Required]
        [Display(Name = "ValCosto-Total")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double valcosto_total { get; set; }
        [Required]
        [Display(Name = "ValVenta-Total")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double valventa_total { get; set; }

        public string imagen { get; set; }
        [Required]
        [Display(Name = "Unidad-Medida")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string uni_medida { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        [Range(0, 2, ErrorMessage = "Error, Fuera de Rango")]
        public int categoryid { get; set; }
        [Required]
        [Display(Name = "Ruta")]
        [Range(0, 2, ErrorMessage = "Error, Fuera de Rango")]
        public int bhabilitado { get; set; }
        [Required]
        [Display(Name = "Val-Min-Venta")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double valmin_venta { get; set; }
        [Required]
        [Display(Name = "Porcentaje")]
        [Range(0, 100, ErrorMessage = "Error, Fuera de Rango")]
        public double porcentaje { get; set; }

        //Propiedades Adicionales
        public string file { get; set; }
    }
}