using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Domain;
using REQFINFO.Repository;

namespace REQFINFO.Business.Interfaces
{
    public interface IUserXProjectXWorkFlowUserGroupBusiness
    {
        UserXProjectXWorkFlowUserGroupModel GetWOrkFlowProjectDetail(int IDUPW);
        List<UserXProjectXWorkFlowUserGroupModel> GetWorkFlowUserGroup(Int32 IDWorkFlow, Int32? IDContractor,Int32? Sequence);
        Nullable<int> GetIsUserExistInThisLevel(Int32 IDProject, Int32 userdata, Int32? sequence);
    }
}
