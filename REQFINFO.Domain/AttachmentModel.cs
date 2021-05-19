using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
   public class AttachmentModel
    {
        public int IDAttachment { get; set; }
        public System.Guid IDLifeCycle { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
      
    }
}
