using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class LevelVM
    {
        public string IDLevel { get; set; }
        public string IDWorkFlowUserGroup { get; set; }
        public Nullable<int> Sequence { get; set; }
        public bool ISDisplayContractorGIGNumber { get; set; }
        public bool ISDisplayContractor { get; set; }
        public string Name { get; set; }
        public WorkFlowUserGroupVM WorkFlowUserGroupVM { get; set; }
    }
}