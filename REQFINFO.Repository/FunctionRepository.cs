using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REQFINFO.Repository
{
  public  class FunctionRepository : BaseRepository<Function>
    {
      GIGEntities entities;
      public FunctionRepository(IUnitOfWork unit)
              : base(unit)
          {
              entities = (GIGEntities)this.UnitOfWork.Db;
          }

      public List<RFI_GetActionsForLoggedInUser_Result> GetActionsForLoggedInUser(Int32 UserID, Int32 ProjectID, Guid IDGIG, Int32 IDFunctionTrigger)
      {
          List<RFI_GetActionsForLoggedInUser_Result> ActionsForLoggedInUser = entities.RFI_GetActionsForLoggedInUser(UserID, ProjectID, IDGIG, IDFunctionTrigger).ToList();
          return ActionsForLoggedInUser;
      }
      public List<RFI_GetActionsOnFirstLevel_Result> GetFirstLevelActions(Int32 UserId, Int32 IDProject, Int32 IDWorkFlowUserGroup, Nullable<bool> IsCreatedNotClosed)
      {
          List<RFI_GetActionsOnFirstLevel_Result> FirstLevelActions = entities.RFI_GetActionsOnFirstLevel(UserId, IDProject, IDWorkFlowUserGroup, IsCreatedNotClosed).ToList();
          return FirstLevelActions;
      }
    
    }
}

