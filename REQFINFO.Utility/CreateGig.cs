using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Utility
{
  public  class CreateGig
    {

        public int IDUDFType { get; set; }
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
    }
}
