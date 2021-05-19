
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
    public interface ILifeCycleBusiness
    {
        LevelXFunctionModel GetLevelXFunctionData(Int32 IDUPW);
        LevelXFunctionModel GetIScreatedNotCLosed(Int32? IDLevelXFunction);
        LevelXFunctionModel GetLevelXFunctionID(Int32 IDWorkFlowUserGroup);
        
        Guid GetLifeCycleIdForCreatedGIGByGidId(Guid IDGig);
        Guid SaveLifeCycleData(LifeCycleModel lifeCycleModel);
        LifeCycleModel GetLifeCycleData(Guid IDGig, Int32 IDLevelXFunction);
        List<LifeCycleModel> GetallLifeCycleData(Guid IDGig);
        LifeCycleModel GetActionsRestriction(Guid IDGig);
        LifeCycleModel GetPreparedByAndOrgainator(Guid IDGig);
        LifeCycleModel GetLifecycleData(Guid IDGIG, Int32 IDLevel);
        LifeCycleModel GetPreviousLifeCycle(Guid IDGig, Int32 IDLevel);
        LifeCycleModel GetFIrstLevelCycle(Guid IDGIG);
        LifeCycleModel GetClosedLifeCycle(Guid IDGig);
    }
}
