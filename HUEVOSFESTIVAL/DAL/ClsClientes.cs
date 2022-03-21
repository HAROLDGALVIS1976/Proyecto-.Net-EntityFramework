using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsClientes
    {
        public int idcliente { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Cliente")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string cliente { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Identificacion")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string nuip { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Direccion")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numero de Telefono no Valido")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Ciudad")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string ciudad { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Ingrese un E-Mail Valido")]
        public string email { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Ruta")]
        [Range(0, 2, ErrorMessage = "Seleccione de Forma Correcta")]
        public int idruta { get; set; }

        public int bhabilitado { get; set; }

        // Propiedades Adicionales

        public string mensaje { get; set; }
    }
}