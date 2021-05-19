using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class WorkFLowVM
    {
        public string IDWorkflow { get; set; }
        public string Name { get; set; }
        public Nullable<int> IDProjectTemplate { get; set; }
        public string DisplayName { get; set; }
    }
}