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
    public class FunctionBusiness : FunctionRepository, IFunctionBusiness
    {
        #region property
        private readonly FunctionRepository FunctionsRepository;
        private readonly LevelXFunctionRepository levelXFunctionRepository;
        private readonly LifeCycleRepository lifeCycleRepository;
        #endregion
        public FunctionBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            FunctionsRepository = this;
            levelXFunctionRepository = new LevelXFunctionRepository(_unitOfWork);
            lifeCycleRepository = new LifeCycleRepository(_unitOfWork);

        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public List<FunctionModel> GetGIGTabsList()
        {

            List<Function> function = FunctionsRepository.GetAll().ToList();
            List<FunctionModel> functionModel = new List<FunctionModel>();
            Mapper.Map(function, functionModel);
            return functionModel;
        }

        public List<FunctionModel> GetActionsForLoggedInUser(Int32 IDUser, Int32 ProjectID, Guid IDGIG, Int32 IDFunctionTrigger)
        {
            List<FunctionModel> functionModel = new List<FunctionModel>();


            List<RFI_GetActionsForLoggedInUser_Result> functionList = FunctionsRepository.GetActionsForLoggedInUser(IDUser, ProjectID, IDGIG, IDFunctionTrigger);
            Mapper.Map(functionList, functionModel);
            List<FunctionModel> functionModelList = GetAllActions();
            LifeCycle Lifecyclemodel = lifeCycleRepository.SingleOrDefault( x =>  x.IDGig==IDGIG && x.LevelXFunction.IsCreatedNotClosed==false);
            foreach (FunctionModel fm in functionModelList)
            {
                int staus = 0;
                foreach (FunctionModel funcModelObj in functionModel)
                {
                    if (fm.IDFunction == funcModelObj.IDFunction)
                    {
                        if (Lifecyclemodel != null)
                        {
                            if (Lifecyclemodel.IDLevelXFunction != funcModelObj.IDLevelXFunction)
                            {
                                fm.Enable = true;

                                fm.IDLevelXFunction = funcModelObj.IDLevelXFunction;
                                staus = 1;
                                break;
                            }
                        }
                        else
                        {

                            fm.Enable = true;
                            fm.IDLevelXFunction = funcModelObj.IDLevelXFunction;
                            staus = 1;
                            break;
                        }
                    }

                }
                if (staus == 0)
                {
                    fm.Enable = false;

                }
            }

            return functionModelList;
        }







        public List<FunctionModel> GetFirstLevelActions(Int32 UserId, Int32 IDProject, Int32 IDWorkFlowUserGroup, Nullable<bool> IsCreatedNotClosed)
        {
            List<FunctionModel> functionModel = new List<FunctionModel>();
            List<RFI_GetActionsOnFirstLevel_Result> functionList = FunctionsRepository.GetFirstLevelActions(UserId, IDProject, IDWorkFlowUserGroup, IsCreatedNotClosed);
            Mapper.Map(functionList, functionModel);
          
            return functionModel;
        }

        public List<FunctionModel> GetAllActions()
        {

            List<Function> function = FunctionsRepository.GetAll().OrderByDescending(x =>x.FunctionOrder).ToList();
            List<FunctionModel> functionModel = new List<FunctionModel>();

            foreach (FunctionModel funcModelObj in functionModel)
            {

                funcModelObj.Enable = false;


            }

            Mapper.Map(function, functionModel);
            return functionModel;
        }

        public LevelXFunctionModel GetFunctionDetail(Int32 IDLevelXFunction)
        {
            LevelXFunction levelXFunction = levelXFunctionRepository.SingleOrDefault(x => x.IDLevelXFunction == IDLevelXFunction);
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();
            levelXFunctionModel.FunctionModel = new FunctionModel();
            Mapper.Map(levelXFunction, levelXFunctionModel);
            Mapper.Map(levelXFunction.Function, levelXFunctionModel.FunctionModel);
            return levelXFunctionModel;
        }

    }
}
