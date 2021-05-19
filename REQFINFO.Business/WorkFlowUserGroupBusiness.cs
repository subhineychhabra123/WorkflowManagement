using ExpressMapper;
using REQFINFO.Business.Interfaces;
using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business
{
    public class WorkFlowUserGroupBusiness : WorkFlowUserGroupRepository, IWorkFlowUserGroupBusiness
    {
        private readonly WorkFlowUserGroupRepository workFlowUserGroupRepository;
        public WorkFlowUserGroupBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            workFlowUserGroupRepository = this;
        }
        public List<WorkFlowUserGroupModel> GetWorkFlowUserGroup(Int32 IDWorkFlow)
        {
            List<WorkFlowUserGroupModel> workFlowUserGroupModel = new List<WorkFlowUserGroupModel>();
            List<WorkFlowUserGroup> workFlowUserGroup = workFlowUserGroupRepository.GetAll(x => x.IDWorkFlow == IDWorkFlow).ToList();
            Mapper.Map(workFlowUserGroup, workFlowUserGroupModel);
            return workFlowUserGroupModel;
        }


     

    }
}
