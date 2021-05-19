using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
  public  class FunctionModel
    {

        public int IDFunction { get; set; }
        public int IDLevelXFunction { get; set; }
        public string Name { get; set; }
        public Nullable<int> Direction { get; set; }
        public int IDTab { get; set; }
        public int Sequence { get; set; }
        public string PastTenseFunction { get; set; }
        public bool IsAlignedLeft { get; set; }
        public int FunctionOrder { get; set; }
        public bool Enable { get; set; }
        public Nullable<bool> ISCreatedNotClosed { get; set; }

    }
}
