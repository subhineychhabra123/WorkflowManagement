using System;

using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace REQFINFO.Repository
{
    public class WorkflowRepository : BaseRepository<Workflow>
    {

        GIGEntities entities;
        public WorkflowRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;

        }

        public List<RFI_GetUserWorkflow_Result> GetUserWorkflowList(Int32 UserID, Int32 CompanyId)
        {
            List<RFI_GetUserWorkflow_Result> UserWorkflowList = entities.RFI_GetUserWorkflow(UserID, CompanyId).ToList();
            return UserWorkflowList;
        }

        public int? GetGIGCount(int? IDProject, int? IDTab, int? TabStatus)
        {
            // ref int StatusCount = 0;
            ObjectParameter statusCount = new ObjectParameter("statusCount", typeof(int));
            int count = entities.RFI_GetGIGCommunicationCounts(IDProject, IDTab, TabStatus, statusCount);
            if (statusCount.Value.ToString()!="")
                return Convert.ToInt32(statusCount.Value);
            else
                return 0;
        }
    }
}
