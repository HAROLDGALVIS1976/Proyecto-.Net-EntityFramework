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
    
    public partial class DEVENTA_PROD
    {
        public string IDVENTA { get; set; }
        public string REMISION { get; set; }
        public string REF { get; set; }
        public string DESCRIPCION { get; set; }
        public string NUIP { get; set; }
        public double VALUNI_VENTA { get; set; }
        public double CANT { get; set; }
        public double MONTO_TOTAL { get; set; }
        public string TIPO { get; set; }
        public System.DateTime FECHAVENTA { get; set; }
        public System.DateTime FECHAPAGO { get; set; }
    
        public virtual VENTA_PROD VENTA_PROD { get; set; }
    }
}
