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
    
    public partial class UDFTemplate
    {
        public int IDUDFTemplate { get; set; }
        public int IDWorkflow { get; set; }
        public Nullable<int> IDUDFType { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
        public Nullable<int> IDLookupType { get; set; }
        public bool CanEdit { get; set; }
    
        public virtual LookupType LookupType { get; set; }
        public virtual UDFType UDFType { get; set; }
        public virtual Workflow Workflow { get; set; }
    }
}
