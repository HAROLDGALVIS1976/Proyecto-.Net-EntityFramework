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
    
    public partial class REFERENCIAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REFERENCIAS()
        {
            this.CAMBIOS = new HashSet<CAMBIOS>();
            this.VENTA_PRODWEB = new HashSet<VENTA_PRODWEB>();
        }
    
        public int IDREF { get; set; }
        public string REF { get; set; }
        public string DESCRIPCION { get; set; }
        public double CANT { get; set; }
        public double VALCOSTO_UNI { get; set; }
        public double VALUNI_VENTA { get; set; }
        public double CANT_MINIMA { get; set; }
        public double VALCOSTO_TOTAL { get; set; }
        public double VALVENTA_TOTAL { get; set; }
        public string IMAGEN { get; set; }
        public string UNI_MEDIDA { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BHABILITADO { get; set; }
        public Nullable<double> VALMIN_VENTA { get; set; }
        public Nullable<double> PORCENTAJE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMBIOS> CAMBIOS { get; set; }
        public virtual Tbl_Category Tbl_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VENTA_PRODWEB> VENTA_PRODWEB { get; set; }
    }
}
