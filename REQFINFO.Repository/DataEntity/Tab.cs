//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace REQFINFO.Repository.DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tab
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tab()
        {
            this.Functions = new HashSet<Function>();
            this.FunctionXTabs = new HashSet<FunctionXTab>();
        }
    
        public int IDTab { get; set; }
        public string Tab1 { get; set; }
        public int Sequence { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Function> Functions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FunctionXTab> FunctionXTabs { get; set; }
    }
}
