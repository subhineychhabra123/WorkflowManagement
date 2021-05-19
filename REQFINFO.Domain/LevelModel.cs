using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
   public class LevelModel
    {
        public int IDLevel { get; set; }
        public int IDWorkFlowUserGroup { get; set; }
        public Nullable<int> Sequence { get; set; }
        public bool ISDisplayContractorGIGNumber { get; set; }
        public bool ISDisplayContractor { get; set; }
        public string Name { get; set; }
        public WorkFlowUserGroupModel  WorkFlowUserGroupModel { get; set; }
      
    }
}
