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
    
    public partial class LevelXFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LevelXFunction()
        {
            this.LifeCycles = new HashSet<LifeCycle>();
        }
    
        public int IDLevelXFunction { get; set; }
        public int IDLevel { get; set; }
        public int IDFunction { get; set; }
        public Nullable<bool> IsCreatedNotClosed { get; set; }
        public Nullable<int> IDFunctionTrigger { get; set; }
    
        public virtual Function Function { get; set; }
        public virtual Level Level { get; set; }
        public virtual LevelXFunction LevelXFunction1 { get; set; }
        public virtual LevelXFunction LevelXFunction2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LifeCycle> LifeCycles { get; set; }
    }
}
