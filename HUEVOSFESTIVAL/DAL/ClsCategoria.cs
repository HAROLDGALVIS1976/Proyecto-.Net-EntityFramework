using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HUEVOSFESTIVAL.DAL
{
    public class ClsCategoria
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El Campo no puede estar en Blanco")]
        [Display(Name = "Categoria")]
        [StringLength(50, ErrorMessage = "Minimo 5 and maximo 50 Caracteres", MinimumLength = 5)]
        public string NombreCategoria { get; set; }
        [Required(ErrorMessage = "El Campo no puede estar en Blanco")]
        [Display(Name = "Habilitado")]
        [Range(0, 2, ErrorMessage = "Rango fuera de indices")]
        public int BHABILITADO { get; set; }
    }
}