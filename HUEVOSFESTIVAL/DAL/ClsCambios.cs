using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsCambios
    {

        public int id { get; set; }

        [Display(Name = "EXTR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int extr { get; set; }
        [Display(Name = "AAR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int aar { get; set; }
        [Display(Name = "AR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int ar { get; set; }
        [Display(Name = "BR")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int br { get; set; }
        [Display(Name = "EXTB")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int extb { get; set; }
        [Display(Name = "AAB")]
        [Required]
        [Range(0, 10000, ErrorMessage = "Rango fuera de indices")]
        public int aab { get; set; }
        [Display(Name = "AB")]
        [Required]
        [Range(0, 1000, ErrorMessage = "Rango fuera de indices")]
        public int ab { get; set; }
        [Display(Name = "BB")]
        [Required]
        [Range(0, 1000, ErrorMessage = "Rango fuera de indices")]
        public int bb { get; set; }
        [Display(Name = "Usuario")]
        [Required]
        public int idusuario { get; set; }
        [Display(Name = "Placa Vehiculo")]
        [Required]
        public int idcarro { get; set; }
        [Display(Name = "Cliente")]
        [Required]
        public int idcliente { get; set; }
        [Display(Name = "Ruta")]
        [Required]
        public int idruta { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }

        public int bhabilitado { get; set; }

        public string usuario { get; set; }

        public string placa { get; set; }

        [Display(Name = "Carga")]
        [Required]
        public int idcarga { get; set; }

        //Propiedades Adicionales
        public string Fechacadena { get; set; }
        
        [Display(Name = "Fecha")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaventa { get; set; }
    }
}