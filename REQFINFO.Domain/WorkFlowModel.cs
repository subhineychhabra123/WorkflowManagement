using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
  public  class WorkFlowModel
    {
        public int IDWorkflow { get; set; }
        public string Name { get; set; }
        public Nullable<int> IDProjectTemplate { get; set; }
        public string DisplayName { get; set; }
    }
}
