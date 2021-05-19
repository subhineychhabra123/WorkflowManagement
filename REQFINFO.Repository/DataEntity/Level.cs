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
    
    public partial class Level
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Level()
        {
            this.LevelXFunctions = new HashSet<LevelXFunction>();
            this.LifeCycles = new HashSet<LifeCycle>();
        }
    
        public int IDLevel { get; set; }
        public int IDWorkFlowUserGroup { get; set; }
        public Nullable<int> Sequence { get; set; }
        public bool ISDisplayContractorGIGNumber { get; set; }
        public bool ISDisplayContractor { get; set; }
    
        public virtual WorkFlowUserGroup WorkFlowUserGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LevelXFunction> LevelXFunctions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LifeCycle> LifeCycles { get; set; }
    }
}