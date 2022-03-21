using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsRuta
    {

        public int idruta { get; set; }
        [Required(ErrorMessage = "El Campo no puede estar en Blanco")]
        [Display(Name = "Ruta")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string nombreruta { get; set; }
        [Required(ErrorMessage = "El Campo no puede estar en Blanco")]
        [Display(Name = "Habilitado")]
        [Range(0, 2, ErrorMessage = "Rango fuera de indices")]
        public int bhabilitado { get; set; }
    }
}