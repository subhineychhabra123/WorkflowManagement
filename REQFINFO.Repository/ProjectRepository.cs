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
    public class ProjectRepository :BaseRepository<Project>
    {
        GIGEntities entities;
        public ProjectRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;

        }
        public List<RFI_GetProjectsList_Result> GetUserProjectList(Int32 UserID, Int32 CompanyId, Int32 WorkFlowID)
        {
            List<RFI_GetProjectsList_Result> UserWorkflowList = entities.RFI_GetProjectsList(UserID, CompanyId, WorkFlowID).ToList();
            return UserWorkflowList;
        }
    }
}
