using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Business.Interfaces;
using System.Collections.Generic;
using ExpressMapper;
using System.Linq;
using REQFINFO.Utility;
using System;



namespace REQFINFO.Business
{
    public class UserXProjectXWorkFlowUserGroupBusiness : UserXProjectXWorkFlowUserGroupRepository, IUserXProjectXWorkFlowUserGroupBusiness
    {
    
        #region property
        private readonly UserXProjectXWorkFlowUserGroupRepository UserXProjectXWorkFlowUserGroupRepository;
        private readonly WorkFlowUserGroupRepository workFlowUserGroupRepository;

        #endregion
        public UserXProjectXWorkFlowUserGroupBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            UserXProjectXWorkFlowUserGroupRepository = this;
            workFlowUserGroupRepository = new WorkFlowUserGroupRepository(_unitOfWork);
        
        }


        public UserXProjectXWorkFlowUserGroupModel GetWOrkFlowProjectDetail(int IDUPW)
        {
            try
            {
                UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = new Domain.UserXProjectXWorkFlowUserGroupModel();

                UserXProjectXWorkFlowUserGroup userXProjectXWorkFlowUserGroup = UserXProjectXWorkFlowUserGroupRepository.SingleOrDefault(x => x.IDUPW == IDUPW && x.Active == true);

                userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel = new WorkFlowUserGroupModel();
                Mapper.Map(userXProjectXWorkFlowUserGroup.WorkFlowUserGroup, userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel);
                Mapper.Map(userXProjectXWorkFlowUserGroup, userXProjectXWorkFlowUserGroupModel);
                return userXProjectXWorkFlowUserGroupModel;
            }

            catch
            {
                return null;
            }

        }
        public List<UserXProjectXWorkFlowUserGroupModel> GetWorkFlowUserGroup(Int32 IDWorkFlow, Int32? IDContractor, Int32? Sequence)
        {
            List<UserXProjectXWorkFlowUserGroup> workFlowUserGroup = new List<UserXProjectXWorkFlowUserGroup>();
            List<UserXProjectXWorkFlowUserGroupModel> workFlowUserGroupModel = new List<UserXProjectXWorkFlowUserGroupModel>();
            if (IDContractor != null)
            {
                workFlowUserGroup = UserXProjectXWorkFlowUserGroupRepository.GetAll(x => x.User.IDContractor == IDContractor && x.WorkFlowUserGroup.Workflow.IDWorkflow == IDWorkFlow  ).GroupBy(g => g.IDUser).Select(x => x.FirstOrDefault()).ToList();

            }
            else
            {
                workFlowUserGroup = UserXProjectXWorkFlowUserGroupRepository.GetAll(x => x.WorkFlowUserGroup.Workflow.IDWorkflow == IDWorkFlow).GroupBy(g => g.IDUser).Select(x => x.FirstOrDefault()).ToList();

            }



            Mapper.Map(workFlowUserGroup, workFlowUserGroupModel);
            return workFlowUserGroupModel;
        }
      



        public Nullable<int> GetIsUserExistInThisLeve(Int32 IDProject, Int32 IDUser, Int32? sequence)
        {

            int? IsUserExistInThisLevel = UserXProjectXWorkFlowUserGroupRepository.GetIsUserExistInThisLevel(IDProject, IDUser, sequence);


            return IsUserExistInThisLevel;

        }












       
        
    }
}
