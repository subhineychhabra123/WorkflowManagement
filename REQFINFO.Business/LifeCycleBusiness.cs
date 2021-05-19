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
    public class LifeCycleBusiness : LifeCycleRepository, ILifeCycleBusiness
    {
        private readonly LifeCycleRepository lifeCycleRepository;
        private readonly LevelXFunctionRepository LevelXFunctionRepository;
        public LifeCycleBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            lifeCycleRepository = this;
            LevelXFunctionRepository = new LevelXFunctionRepository(_unitOfWork);
        }
        public LifeCycleModel GetActionsRestriction(Guid IDGig)
        {
            LifeCycle lifecycle = lifeCycleRepository.GetAll(x => x.IDGig == IDGig).OrderByDescending(x => x.LoggedOn).Take(1).ToList().SingleOrDefault();
            LifeCycle lifecycleobj = lifeCycleRepository.GetAll(x => x.IDGig == IDGig && x.AssignedToID != null).OrderByDescending(x => x.LoggedOn).Take(1).ToList().SingleOrDefault();

            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            lifecycle.AssignedToID = lifecycleobj.AssignedToID;

            Mapper.Map(lifecycle, lifeCycleModel);

            lifeCycleModel.LevelModel = new LevelModel();
            if (lifeCycleModel != null)
            {
                Mapper.Map(lifecycle.Level, lifeCycleModel.LevelModel);

            }
            return lifeCycleModel;
        }



        public LifeCycleModel GetPreviousLifeCycle(Guid IDGig, Int32 IDLevel)
        {
            LifeCycle lifecycle = lifeCycleRepository.GetAll(x => x.IDGig == IDGig && x.IDLevel == IDLevel).OrderByDescending(x => x.LoggedOn).Take(1).ToList().SingleOrDefault();
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            Mapper.Map(lifecycle, lifeCycleModel);
            return lifeCycleModel;
        }

        public LifeCycleModel GetClosedLifeCycle(Guid IDGig)
        {
            LifeCycle lifecycle = lifeCycleRepository.SingleOrDefault(x => x.IDGig == IDGig && x.AssignedToID == null);
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            Mapper.Map(lifecycle, lifeCycleModel);
            return lifeCycleModel;
        }


        public LifeCycleModel GetPreparedByAndOrgainator(Guid IDGig)
        {
            LifeCycle lifecycle = lifeCycleRepository.GetAll(x => x.IDGig == IDGig).OrderBy(x => x.LoggedOn).Take(1).ToList().SingleOrDefault();
            LifeCycleModel lifeCycleModel = new LifeCycleModel();


            Mapper.Map(lifecycle, lifeCycleModel);

            lifeCycleModel.LevelModel = new LevelModel();
            if (lifeCycleModel != null)
            {
                Mapper.Map(lifecycle.Level, lifeCycleModel.LevelModel);

            }

            lifeCycleModel.User = new UserModel();
            lifeCycleModel.User1 = new UserModel();
            lifeCycleModel.User2 = new UserModel();
            if (lifeCycleModel != null)
            {
                Mapper.Map(lifecycle.User, lifeCycleModel.User);
                Mapper.Map(lifecycle.User1, lifeCycleModel.User1);
                Mapper.Map(lifecycle.User2, lifeCycleModel.User2);
            }

            return lifeCycleModel;
        }


        public LevelXFunctionModel GetLevelXFunctionData(Int32 IDUPW)
        {

            RFI_GetLevelXFunctionData_Result GetLevelXFunctionData = lifeCycleRepository.GetLevelXFunctionData(IDUPW);
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();

            Mapper.Map(GetLevelXFunctionData, levelXFunctionModel);
            return levelXFunctionModel;
        }



        public LevelXFunctionModel GetIScreatedNotCLosed(Int32? IDLevelXFunction)
        {
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();
            LevelXFunction LevelXFunction = LevelXFunctionRepository.SingleOrDefault(x => x.IDLevelXFunction == IDLevelXFunction);


            Mapper.Map(LevelXFunction, levelXFunctionModel);
            return levelXFunctionModel;
        }




        public Guid SaveLifeCycleData(LifeCycleModel lifeCycleModel)
        {
            LifeCycle lifeCycle = new LifeCycle();

            lifeCycle = lifeCycleRepository.SingleOrDefault(x => x.IDLifeCycle == lifeCycleModel.IDLifeCycle);
            if (lifeCycle != null)
            {
                Mapper.Map(lifeCycleModel, lifeCycle);
                lifeCycleRepository.Update(lifeCycle);
                return lifeCycle.IDLifeCycle;
            }
            else
            {
                LifeCycle lifeCycleObj = new LifeCycle();
                Mapper.Map(lifeCycleModel, lifeCycleObj);

                lifeCycleRepository.Insert(lifeCycleObj);
                return lifeCycleObj.IDLifeCycle;
            }
        }
        public Guid GetLifeCycleIdForCreatedGIGByGidId(Guid IDGig)
        {
            Guid lifecycleId = lifeCycleRepository.GetAll(x => x.IDGig == IDGig).OrderBy(x => x.GIG.DateCreated).Select(x => x.IDLifeCycle).ToList().FirstOrDefault();
            return lifecycleId;
        }
        public LifeCycleModel GetLifeCycleData(Guid IDGig, Int32 IDLevelXFunction)
        {
            LifeCycle LifeCycleData = lifeCycleRepository.SingleOrDefault(x => x.IDGig == IDGig && x.IDLevelXFunction == IDLevelXFunction);
            LifeCycleModel LifeCycleModel = new LifeCycleModel();
            LifeCycleModel.AttachmentModel = new List<AttachmentModel>();
            if (LifeCycleData != null)
            {
                Mapper.Map(LifeCycleData.Attachments, LifeCycleModel.AttachmentModel);
            }
            Mapper.Map(LifeCycleData, LifeCycleModel);
            return LifeCycleModel;

        }
        public List<LifeCycleModel> GetallLifeCycleData(Guid IDGig)
        {
            List<RFI_GetGIGLogData_Result> LifeCycleData = lifeCycleRepository.GetallLifeCycleData(IDGig).ToList();
            List<LifeCycleModel> LifeCycleModel = new List<LifeCycleModel>();
            LifeCycleModel LifeCycleModelobj = new LifeCycleModel();
            foreach (var LifeCycleModelObj in LifeCycleModel)
            {
                LifeCycleModelobj.AttachmentModel = new List<AttachmentModel>();
                if (LifeCycleModelObj.AttachmentModel != null)
                {
                    Mapper.Map(LifeCycleModelobj.AttachmentModel, LifeCycleModelObj.AttachmentModel);
                }

            }


            Mapper.Map(LifeCycleData, LifeCycleModel);
            return LifeCycleModel;

        }

        public LevelXFunctionModel GetLevelXFunctionID(Int32 IDWorkFlowUserGroup)
        {

            RFI_GetCreateOption_Result GetLevelXFunctionID = lifeCycleRepository.GetLevelXFunctionID(IDWorkFlowUserGroup);
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();

            Mapper.Map(GetLevelXFunctionID, levelXFunctionModel);
            return levelXFunctionModel;
        }
        public LifeCycleModel GetLifecycleData(Guid IDGIG, Int32 IDLevel)
        {

            LifeCycle GetLevelXFunctionData = lifeCycleRepository.GetAll(x => x.IDLevel == IDLevel && x.IDGig == IDGIG).OrderByDescending(x => x.LoggedOn).SingleOrDefault();
            LifeCycleModel LifeCycleModel = new LifeCycleModel();

            Mapper.Map(GetLevelXFunctionData, LifeCycleModel);
            return LifeCycleModel;
        }


        public LifeCycleModel GetFIrstLevelCycle(Guid IDGIG)
        {

            LifeCycle GetFIrstLevelCycle = lifeCycleRepository.GetAll(x => x.IDGig == IDGIG).OrderBy(x=>x.LoggedOn).Take(1).SingleOrDefault();
            LifeCycleModel LifeCycleModel = new LifeCycleModel();

            Mapper.Map(GetFIrstLevelCycle, LifeCycleModel);
            return LifeCycleModel;
        }
    }


}
