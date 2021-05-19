using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Business.Interfaces;
using System.Collections.Generic;
using ExpressMapper;
using REQFINFO.Utility;
using System;

namespace REQFINFO.Business
{
    public class WorkflowBusiness : WorkflowRepository, IWorkflowBusiness
    {
    
        #region property
        private readonly WorkflowRepository workflowRepository;
    
        #endregion
        public WorkflowBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            workflowRepository = this;
           
        }


      

        public List<WorkFlowModel> GetUserWorkflowList(int userID, int companyId)
        {
            List<WorkFlowModel> workFlowModel= new List<WorkFlowModel>();
            List<RFI_GetUserWorkflow_Result> UserWorkflowList = workflowRepository.GetUserWorkflowList(userID, companyId);
            Mapper.Map(UserWorkflowList, workFlowModel);
            return workFlowModel;
           
        }

        public Nullable<int> GetGIGCount(int? IDProjects, int? IDTab, int? TabStatus)
        {

            int? UserWorkflowList = workflowRepository.GetGIGCount(IDProjects, IDTab, TabStatus);

            return UserWorkflowList;

        }
        public WorkFlowModel GetWorkflowNamewithID(int workflowID)
        {
            WorkFlowModel workFlowModel = new WorkFlowModel();
            Workflow Workflow = workflowRepository.SingleOrDefault(x => x.IDWorkflow == workflowID);
            Mapper.Map(Workflow, workFlowModel);
          
            return workFlowModel;

        }

        
    }
}
