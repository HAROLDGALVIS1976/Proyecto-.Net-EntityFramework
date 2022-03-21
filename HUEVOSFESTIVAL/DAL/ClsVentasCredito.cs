using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsVentasCredito
    {
        public int id { get; set; }

        [Display(Name = "Remision")]
        [Required]
        public string Remision { get; set; }

        [Display(Name = "Nuip")]
        [Required]
        public string Nuip { get; set; }

        [Display(Name = "Deuda")]
        [Required]
        [Range(0, 10000000, ErrorMessage = "Error, Fuera de Rango")]
        public double Deuda { get; set; }

        [Display(Name = "Tipo Venta")]
        [Required]
        public string Tipo { get; set; }

        [Display(Name = "Fecha Pago")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechapago { get; set; }

        [Display(Name = "Cliente")]
        [Required]
        public int idcliente { get; set; }

        public int bhabilitado { get; set; }

        [Display(Name = "Placa")]
        [Required]
        public int idcarro { get; set; }

        [Display(Name = "Consecutivo")]
        [Required]
        public string consecutivo { get; set; }


        //Propiedades Adicionales
        public string fechapagocadena { get; set; }

        [Display(Name = "Resta")]
        [Required]
        [Range(0, 100000000, ErrorMessage = "Error, Fuera de Rango")]
        public double monto_total { get; set; }

        [Display(Name = "Abono")]
        [Required]
        [Range(0, 100000000, ErrorMessage = "Error, Fuera de Rango")]
        public double Abono { get; set; }



    }
}