using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
    public class CreateGigModel
    {
        public int IDUDFType { get; set; }
        public string Name { get; set; }
        public Nullable<byte> IsRequired { get; set; }
        public int IDUDFDefinition { get; set; }
        public int IDWorkflow { get; set; }
        public int IDProject { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
        public string DefaultValue { get; set; }
        public int IDLookupType { get; set; }
        public System.Guid IDUDFInstance { get; set; }
        public Nullable<byte> CanEdit { get; set; }
        public string Val1 { get; set; }
        public string CustomLookUpFieldName { get; set; }
        public string CustomLookupName { get; set; }
        public List<CustomLookupModel> CustomLookupModel { get; set; }
        public List<UDFFieldsValidationsModel> UDFFieldsValidations { get; set; }
    }


    

}
