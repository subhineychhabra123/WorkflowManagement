using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class UserXProjectXWorkFlowUserGroupVM
    {



        public string IDUPW { get; set; }
        public string IDUser { get; set; }
        public string IDProject { get; set; }
        public string IDWorkFlowUserGroup { get; set; }
        public bool Active { get; set; }
        public string ProjectName { get; set; }
        public string WorkflowName { get; set; }
        public string WorkflowDisplayName { get; set; }
        public WorkFlowUserGroupVM workFlowUserGroupVM { get; set; }
        public UserVM userVM { get; set; }
        public string UserName { get; set; }
    }
}