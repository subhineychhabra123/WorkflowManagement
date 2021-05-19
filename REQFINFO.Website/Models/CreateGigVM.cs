using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class CreateGigVM
    {
        public int IDUDFType { get; set; }
        public string IDGIg { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public string IDUDFDefinition { get; set; }
        public int IDWorkflow { get; set; }
        public int IDProject { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
        public string DefaultValue { get; set; }
        public int IDLookupType { get; set; }
        public Nullable<bool> CanEdit { get; set; }
        public System.Guid IDUDFInstance { get; set; }
        public string Val1 { get; set; }
        public string CustomLookupName { get; set; }
        public List<CustomLookupVM> CustomLookupModel { get; set; }
        public List<UDFFieldsValidationsVM> UDFFieldsValidations { get; set; }
        
    }
}