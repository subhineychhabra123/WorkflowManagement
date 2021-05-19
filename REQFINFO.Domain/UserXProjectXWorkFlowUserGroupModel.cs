using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
   public  class UserXProjectXWorkFlowUserGroupModel
    {

        public int  IDUPW { get; set; }
        public int IDUser { get; set; }
        public int IDProject { get; set; }
        public int IDWorkFlowUserGroup { get; set; }
        public int IDWorkFlow { get; set; }
        public bool Active { get; set; }
        public string  ProjectName { get; set; }
        public string WorkflowName { get; set; }
        public string WorkflowDisplayName { get; set; }
        public string UserName { get; set; }
        public WorkFlowUserGroupModel workFlowUserGroupModel { get; set; }
        public UserModel userModel { get; set; }

    }
}
