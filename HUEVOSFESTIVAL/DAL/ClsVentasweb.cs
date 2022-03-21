using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsVentasweb
    {
        public int id { get; set; }

        [Display(Name = "Referencia")]
        public int idref { get; set; }

        [Display(Name = "Ciente")]
        [Required]
        public int idcliente { get; set; }

        [Display(Name = "Ruta")]
        [Required]
        public int idruta { get; set; }

        [Display(Name = "TipoVenta")]
        [Required]
        public int idtipoventa { get; set; }

        [Required]
        public string Ref { get; set; }

        [Required]
        public string cliente { get; set; }

        [Required]
        public string ruta { get; set; }

        [Required]
        public string tipo { get; set; }

        [Required]
        [Display(Name = "ValVenta-Total")]
        //[Range(typeof(double), "1", "1000000", ErrorMessage = "Cantidad fuera de Rango")]
        // [Range(0, double.MaxValue, ErrorMessage = "Valor invalido")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public  double valuni_venta { get; set; }

        [Display(Name = "Cantidad")]
        [Required]
        // [Range(typeof(double), "1", "1000000", ErrorMessage = "Cantidad fuera de Rango")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double cant { get; set; }


        [Display(Name = "MontoVenta")]
        [Required]
        //[Range(0, 10000000, ErrorMessage = "Rango fuera de indices")]
        //[Range(typeof(double), "1", "10000000", ErrorMessage = "Carga Invalida")]
        [Range(0, 100000000, ErrorMessage = "Error, Fuera de Rango")]
        public double monto_total { get; set; }

        [Display(Name = "FechaVenta")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaventa { get; set; }

        [Display(Name = "FechaPago")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechapago { get; set; }

        [Display(Name = "Usuario")]
        [Required]
        public int idusuario { get; set; }

        public int bhabilitado { get; set; }

        [Display(Name = "Placa")]
        [Required]
        public int idcarro { get; set; }

        [Display(Name = "Consecutivo")]
        [Required]
        public string consecutivo { get; set; }

        [Display(Name = "Identificador")]
        [Required]
        public int idcarga { get; set; }

        [Display(Name = "Abono")]
        [Required]
        //[Range(typeof(int), "1", "1000000", ErrorMessage = "Carga Invalida")]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double abono { get; set; }

        //Propiedades Adicionales

        public int ClienteParametro { get; set; }

        public string fechaVentaCadena { get; set; }

        public string fechaPagoCadena { get; set; }

        public string placa { get; set; }







    }
}