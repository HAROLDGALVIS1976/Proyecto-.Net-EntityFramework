//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HUEVOSFESTIVAL.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAMBIOS
    {
        public int ID { get; set; }
        public int IDCARGA { get; set; }
        public int IDCLIENTE { get; set; }
        public int IDREF { get; set; }
        public int CANT { get; set; }
        public int IDUSUARIO { get; set; }
        public System.DateTime FECHA { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
    
        public virtual CARGUE CARGUE { get; set; }
        public virtual CLIENTES CLIENTES { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
        public virtual REFERENCIAS REFERENCIAS { get; set; }
    }
}
