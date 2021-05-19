using REQFINFO.Repository.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class LevelXFunctionVM
    {
        public string IDLevelXFunction { get; set; }
        public int IDLevel { get; set; }
        public int IDFunction { get; set; }
        public Nullable<bool> IsCreatedNotClosed { get; set; }
        public int sequence { get; set; }
        
        //public  Function Function { get; set; }
        //public  Level Level { get; set; }
        //public virtual ICollection<LifeCycle> LifeCycles { get; set; }
    }
}