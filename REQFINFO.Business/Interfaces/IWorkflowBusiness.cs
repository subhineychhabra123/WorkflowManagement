using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REQFINFO.Business.Interfaces
{
    public interface IWorkflowBusiness
    {

        List<WorkFlowModel> GetUserWorkflowList(Int32 IDUser, Int32 IDCompany);
        Nullable<int> GetGIGCount(int? IDProjects, int? IDTab, int? TabStatus);
        WorkFlowModel GetWorkflowNamewithID(int workflowID);
    }
}
