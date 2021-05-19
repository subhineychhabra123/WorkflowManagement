using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Domain;

namespace REQFINFO.Business.Interfaces
{
    public interface IFunctionBusiness
    {
      //  List<FunctionModel> GetGIGTabsList();
        List<FunctionModel> GetActionsForLoggedInUser(Int32 IDUser, Int32 ProjectID, Guid IDGIG, Int32 IDFunctionTrigger);
        List<FunctionModel> GetAllActions();
        List<FunctionModel> GetFirstLevelActions(Int32 UserId, Int32 IDProject, Int32 IDWorkFlowUserGroup, Nullable<bool> IsCreatedNotClosed);
        LevelXFunctionModel GetFunctionDetail(Int32 IDLevelXFunction);
    }
}
