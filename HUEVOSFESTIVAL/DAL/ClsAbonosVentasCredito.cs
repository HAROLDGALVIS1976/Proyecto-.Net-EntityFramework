using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsAbonosVentasCredito
    {
        public int ID { get; set; }

        [Required]
        public string remision { get; set; }

        [Display(Name = "Nuip")]
        [Required]
        public string nuip { get; set; }

        [Display(Name = "Abono")]
        [Required]
        [Range(0, 10000000, ErrorMessage = "Rango fuera de indices")]
        public double abono { get; set; }

        [Display(Name = "Tipo Venta")]
        [Required]
        public string tipo { get; set; }

        [Display(Name = "Fecha Abono")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechabono { get; set; }

        [Display(Name = "Fecha Pago")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechapago { get; set; }

        [Display(Name = "Usuario")]
        [Required]
        public int idusuario { get; set; }

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

        //propiedades dicionales

        [Required]
        public string Titulo { get; set; }

    }
}