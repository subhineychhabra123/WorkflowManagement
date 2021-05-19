using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Repository.Infrastructure.Contract;

namespace REQFINFO.Business.Interfaces
{
    public interface IWorkFlowUserGroupBusiness
    {
        List<WorkFlowUserGroupModel> GetWorkFlowUserGroup(Int32 IDWorkFlow);
    }
}
