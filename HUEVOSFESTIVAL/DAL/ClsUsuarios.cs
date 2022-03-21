using System.ComponentModel.DataAnnotations;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsUsuarios
    {
        public int idusuario { get; set; }

        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Usuario")]
        [StringLength(50, ErrorMessage = "Minimo 4 and maximo 50 Caracteres", MinimumLength = 4)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Este Campo no puede estar en Blanco")]
        [Display(Name = "Contraseña")]
        [StringLength(50, ErrorMessage = "Minimo 4 and maximo 50 Caracteres", MinimumLength = 4)]
        public string contraseña { get; set; }
        [Required]
        [Display(Name = "TipoUsuario")]
        [StringLength(1, ErrorMessage = "Minimo 1 and maximo 1 Caracteres", MinimumLength = 1)]
        public string tipousuario { get; set; }
       
        public int iid { get; set; }

        public int iidrol { get; set; }

        public int bhabilitado { get; set; }

        public string mensaje { get; set; }


        //Propiedades  Adicionales
        public string nombrePersona { get; set; }
        public string nombreRol { get; set; }
        public string nombreTipoEmpleado { get; set; }

        public string nombrePersonaEnviar { get; set; }
    }
}