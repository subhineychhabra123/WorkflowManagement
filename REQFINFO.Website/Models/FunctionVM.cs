using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class FunctionVM
    {

        public string IDFunction { get; set; }
        public string IDLevelXFunction { get; set; }
        public string Name { get; set; }
        public Nullable<int> Direction { get; set; }
        public string IDTab { get; set; }
        public int Sequence { get; set; }
        public string PastTenseFunction { get; set; }
        public bool IsAlignedLeft { get; set; }
        public int FunctionOrder { get; set; }
        public bool Enable { get; set; }
        public Nullable<bool> ISCreatedNotClosed { get; set; }

        
    }
}