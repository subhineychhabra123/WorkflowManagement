using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
   public class WorkFlowUserGroupModel
    {
        public int IDWorkFlowUserGroup { get; set; }
        public int IDWorkFlow { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
     
    }
}
