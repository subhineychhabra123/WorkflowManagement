using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REQFINFO.Domain
{
   public class LevelXFunctionModel
    {
        public int IDLevelXFunction { get; set; }
        public int IDLevel { get; set; }
        public int IDFunction { get; set; }
        public Nullable<bool> IsCreatedNotClosed { get; set; }
        public int sequence { get; set; }
        public FunctionModel FunctionModel { get; set; }
    }
}
