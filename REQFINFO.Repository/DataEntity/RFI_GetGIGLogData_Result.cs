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
    
    public partial class RFI_GetGIGLogData_Result
    {
        public string PastTenseFunction { get; set; }
        public string Detail { get; set; }
        public System.Guid IDLifeCycle { get; set; }
        public string loggedbyFirstName { get; set; }
        public string loggedbyLastName { get; set; }
        public string SendtoFirstName { get; set; }
        public string SendtoLastName { get; set; }
        public Nullable<int> Attachment_Count { get; set; }
        public System.DateTime LoggedOn { get; set; }
        public string ImageName { get; set; }
    }
}