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
    
    public partial class Workflow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workflow()
        {
            this.UDFDefinitions = new HashSet<UDFDefinition>();
            this.UDFTemplates = new HashSet<UDFTemplate>();
            this.WorkFlowUserGroups = new HashSet<WorkFlowUserGroup>();
        }
    
        public int IDWorkflow { get; set; }
        public string Name { get; set; }
        public Nullable<int> IDProjectTemplate { get; set; }
        public string DisplayName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UDFDefinition> UDFDefinitions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UDFTemplate> UDFTemplates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkFlowUserGroup> WorkFlowUserGroups { get; set; }
    }
}
