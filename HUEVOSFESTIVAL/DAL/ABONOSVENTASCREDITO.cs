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
    
    public partial class ABONOSVENTASCREDITO
    {
        public int ID { get; set; }
        public string REMISION { get; set; }
        public string NUIP { get; set; }
        public double ABONO { get; set; }
        public string TIPO { get; set; }
        public System.DateTime FECHABONO { get; set; }
        public System.DateTime FECHAPAGO { get; set; }
        public int IDUSUARIO { get; set; }
        public int IDCLIENTE { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
        public int IDCARRO { get; set; }
        public string CONSECUTIVO { get; set; }
    
        public virtual CARROS CARROS { get; set; }
        public virtual CLIENTES CLIENTES { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
    }
}