using REQFINFO.Business;
using REQFINFO.Business.Interfaces;
using REQFINFO.Domain;
using System;
using REQFINFO.Website.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressMapper;
using REQFINFO.Utility;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using REQFINFO.Repository.DataEntity;

namespace REQFINFO.Website.Controllers
{
    public class UserController : Controller
    {

        // GET: /User/
        int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PagingPageSize"]);
        private IWorkflowBusiness WorkFlowBusiness;
        private IFunctionBusiness FunctionBusiness;
        private IGIGBusiness GIGBusiness;
        private IProjectBusiness ProjectBusiness;
        private IStatusBusiness statusBusiness;
        private IUserXProjectXWorkFlowUserGroupBusiness UserXProjectXWorkFlowUserGroupBusiness;
        private IUDFValidationDefinitionBusiness IUDFValidationDefinitionBusiness;
        private ICreateGIGBusiness CreateGIGBusiness;
        private IUDFInstanceBusiness uDFInstanceBusiness;
        private IContractorBusiness contractorBusiness;
        private IAttachmentBusiness attachmentBusiness;
        private ILifeCycleBusiness lifeCycleBusiness;
        private ILifeCycleBusiness LifeCycleBusiness;
        private IWorkFlowUserGroupBusiness workFlowUserGroupBusiness;
        private IUserBusiness userBusiness;
        private ILevelBusiness levelBusiness;

        public UserController(IUserBusiness _userBusiness, ILevelBusiness _levelBusiness, IWorkFlowUserGroupBusiness _workFlowUserGroupBusiness, ILifeCycleBusiness _LifeCycleBusiness, ILifeCycleBusiness _lifeCycleBusiness, IAttachmentBusiness _IAttachmentBusiness, IContractorBusiness _ContractorBusiness, IUDFInstanceBusiness _UDFInstanceBusiness, IProjectBusiness _ProjectBusiness, IWorkflowBusiness _WorkFlowBusiness, IGIGBusiness _GIGBusiness, IFunctionBusiness _IFunctionBusiness, IStatusBusiness _IStatusBusiness, IUserXProjectXWorkFlowUserGroupBusiness _IUserXProjectXWorkFlowUserGroupBusiness, ICreateGIGBusiness _ICreateGIGBusiness, IUDFValidationDefinitionBusiness _IUDFValidationDefinitionBusiness)
        {
            ProjectBusiness = _ProjectBusiness;
            lifeCycleBusiness = _lifeCycleBusiness;
            WorkFlowBusiness = _WorkFlowBusiness;
            GIGBusiness = _GIGBusiness;
            FunctionBusiness = _IFunctionBusiness;
            statusBusiness = _IStatusBusiness;
            UserXProjectXWorkFlowUserGroupBusiness = _IUserXProjectXWorkFlowUserGroupBusiness;
            CreateGIGBusiness = _ICreateGIGBusiness;
            workFlowUserGroupBusiness = _workFlowUserGroupBusiness;
            IUDFValidationDefinitionBusiness = _IUDFValidationDefinitionBusiness;
            uDFInstanceBusiness = _UDFInstanceBusiness;
            contractorBusiness = _ContractorBusiness;
            attachmentBusiness = _IAttachmentBusiness;
            LifeCycleBusiness = _LifeCycleBusiness;
            levelBusiness = _levelBusiness;
            userBusiness = _userBusiness;
        }

        public ActionResult WorkFLow()
        {
            var UserId = SessionManagement.LoggedInUser.IDUser;
            if (UserId == 0)
            {
                return RedirectToAction("login", "site");
            }
            return View();
        }
        /// <summary>
        /// Get current user workflows
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserWorkFlowList()
        {
            var UserId = SessionManagement.LoggedInUser.IDUser;
            var CompanyId = SessionManagement.LoggedInUser.IDCompany;
            List<WorkFLowVM> workFlowVM = new List<WorkFLowVM>();
            List<WorkFlowModel> workFlowModel = WorkFlowBusiness.GetUserWorkflowList(UserId, CompanyId);
            Mapper.Map(workFlowModel, workFlowVM);
            return Json(workFlowVM, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Logout function
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Site");
        }
        public ActionResult Projects(string id)
        {
            var UserId = SessionManagement.LoggedInUser.IDUser;
            if (UserId == 0)
            {
                return RedirectToAction("login", "site");
            }
            ViewBag.WorkFLowID = id;
            return View();
        }
        public JsonResult GetProjectsList(string WorkFLowID)
        {
            Int32 WorkflowID = WorkFLowID.Decrypt();
            var UserId = SessionManagement.LoggedInUser.IDUser;
            WorkFlowModel workFlowModel = WorkFlowBusiness.GetWorkflowNamewithID(WorkflowID);
            var CompanyId = SessionManagement.LoggedInUser.IDCompany;
            List<ProjectVM> ProjectVM = new List<ProjectVM>();
            List<ProjectModel> ProjectModel = ProjectBusiness.GetProjectsList(UserId, CompanyId, WorkflowID);
            Mapper.Map(ProjectModel, ProjectVM);
            return Json(new
            {
                ProjectVM = ProjectVM,
                WorkflowName = workFlowModel.DisplayName,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GIGCommunicationLog(string IDUPW, string IDProject)
        {
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            var UserId = SessionManagement.LoggedInUser.IDUser;
            if (UserId == 0)
            {
                return RedirectToAction("login", "site");
            }
            ViewBag.IDUPW = IDUPW;
            ViewBag.IDProject = IDProject;
            ViewBag.IDWorkflowUerGroup = userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup;
            return View(UserXProjectXWorkFlowUserGroupVM);
        }
        public JsonResult GetAttachments(Guid IDLifeCycle)
        {
            List<AttachmentModel> attachmentModelLst = attachmentBusiness.GetAttachments(IDLifeCycle);
            List<AttachmentVM> attachmentVmlist = new List<AttachmentVM>();
            Mapper.Map(attachmentModelLst, attachmentVmlist);
            return Json(attachmentVmlist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGIGLog(string IDUPW, string IDProject, int PageNumber, string Search, string SortParameter, bool SortOrder, string Searchby)
        {
            List<StatusModel> TabsList = statusBusiness.GetAllStatus();
            Int32 SETIDUPW = IDUPW.Decrypt();
            Int32 IDProjects = IDProject.Decrypt();
            int TabStatus = 0;
            int? tabOpenCount = 0, tabCloseCount = 0, AllGIGCount = 0;
            foreach (var Tabs in TabsList)
            {
                if (Tabs.IDTab == 1)
                {
                    TabStatus = 3;
                    tabCloseCount = WorkFlowBusiness.GetGIGCount(IDProjects, Tabs.IDTab, TabStatus);
                }
                else if (Tabs.IDTab == 3)
                {
                    TabStatus = 2;
                    tabOpenCount = WorkFlowBusiness.GetGIGCount(IDProjects, Tabs.IDTab, TabStatus);
                }
                else
                {
                    TabStatus = 1;
                    AllGIGCount = WorkFlowBusiness.GetGIGCount(IDProjects, 0, TabStatus);
                }
            }
            int totalRecord = 0;
            List<GIGVM> GIGVM = GetCommunicationsDetail(IDProjects, TabsList[0].IDTab, PageNumber, Search, SortParameter, SortOrder, Searchby, ref totalRecord);
            return Json(new
            {
                pageSize = pageSize,
                GIGVM = GIGVM,
                TabsList = TabsList,
                tabOpenCount = tabOpenCount,
                tabCloseCount = tabCloseCount,
                AllGIGCount = AllGIGCount,
                TotalRecords = totalRecord
            }, JsonRequestBehavior.AllowGet);
        }
        public List<GIGVM> GetCommunicationsDetail(Int32 IDUPW, Int32 IDStatus, int PageNumber, string Search, string SortParameter, bool SortOrder, string Searchby, ref int totalRecord)
        {
            List<GIGVM> GIGVM = new List<GIGVM>();
            List<GIGModel> GIGModel = GIGBusiness.GIGCommunicationLog(IDUPW, IDStatus, PageNumber, Search, SortParameter, SortOrder, pageSize, Searchby, ref totalRecord);
            Mapper.Map(GIGModel, GIGVM);
            return GIGVM;
        }
        /// <summary>
        /// On tab click......
        /// </summary>
        /// <param name="IDUPW"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public JsonResult GetCommunications(string IDUPW, int Status, int PageNumber, string Search, string SortParameter, bool SortOrder, string Searchby)
        {
            Int32 SETIDUPW = IDUPW.Decrypt();
            int totalRecord = 0;
            List<GIGVM> GIGVM = GetCommunicationsDetail(SETIDUPW, Status, PageNumber, Search, SortParameter, SortOrder, Searchby, ref totalRecord);
            return Json(new { pageSize = pageSize, GIGVM = GIGVM, TotalRecords = totalRecord }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetActionsForLoggedInUser(string IDProject, Guid IDGIG, string IDUPW, string IDFunctionTrigger)
        {
            List<FunctionVM> functionVM = GetActionsForLoggedInUsers(IDProject, IDGIG, IDUPW, IDFunctionTrigger);
            return Json(functionVM, JsonRequestBehavior.AllowGet);
        }
        public List<FunctionVM> GetActionsForLoggedInUsers(string IDProject, Guid IDGIG, string IDUPW, string IDFunctionTrigger)
        {
                Int32 ProjectID = IDProject.Decrypt();
            Int32 UserId = SessionManagement.LoggedInUser.IDUser;
            List<FunctionVM> functionVM = new List<FunctionVM>();
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();
            //LifeCycleModel ClosedLifeCycle = lifeCycleBusiness.GetClosedLifeCycle(IDGIG);
            List<FunctionModel> functionModel = FunctionBusiness.GetActionsForLoggedInUser(UserId, ProjectID, IDGIG, IDFunctionTrigger.Decrypt());
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            if (userXProjectXWorkFlowUserGroupModel != null)
            {
                levelXFunctionModel = LifeCycleBusiness.GetLevelXFunctionData(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup);

            } Mapper.Map(functionModel, functionVM);
            Int32 userdata = (int)SessionManagement.LoggedInUser.IDUser;
            LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetActionsRestriction(IDGIG);
            int? IsUserExistInThisLevel = UserXProjectXWorkFlowUserGroupBusiness.GetIsUserExistInThisLevel(IDProject.Decrypt(), userdata, lifecyclemodelobjdata.LevelModel.Sequence);
            if (userdata == lifecyclemodelobjdata.AssignedToID && IsUserExistInThisLevel != 0)
            {
              
               
                functionVM = functionVM.Where(x => x.ISCreatedNotClosed != true).ToList().ToList();
                    
            }
            else
            {
                functionVM = null;
            }
            return functionVM;
        }
        public JsonResult GetAllActions()
        {
            List<FunctionVM> functionVM = new List<FunctionVM>();
            List<FunctionModel> functionModel = FunctionBusiness.GetAllActions();
            functionModel = functionModel.Where(x => x.Name != "Create" && x.Name != "create").ToList();
            Mapper.Map(functionModel, functionVM);
            return Json(functionVM, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateGIG(string IDUPW, string IDProject, string IDLevelXFunction)
        {
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            var UserId = SessionManagement.LoggedInUser.IDUser;
            if (UserId == 0)
            {

                return RedirectToAction("login", "site");
            }
            ViewBag.IDLevelXFunction = IDLevelXFunction;
            ViewBag.IDWorkflowUserGroup = UserXProjectXWorkFlowUserGroupVM.IDWorkFlowUserGroup;
            ViewBag.IDUPW = IDUPW;
            ViewBag.IDProject = IDProject;
            return View(UserXProjectXWorkFlowUserGroupVM);
        }
        public JsonResult GetGIGCreateData(string IDUPW, string IDProject)
        {

            // var Fields = GetGIGCreateDynamicFields(IDUPW, IDProject);

            List<ContractorVM> contractorVM = new List<ContractorVM>();
            List<ContractorModel> Contractors = contractorBusiness.GetContractors();
            Mapper.Map(Contractors, contractorVM);
            //GetDataForFields

            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM = new WorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            Mapper.Map(userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM);
            Int32? IDContractor = null;



            LevelModel LevelObj = GetNextLevel(userXProjectXWorkFlowUserGroupModel.IDProject, userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, true);
            List<UserXProjectXWorkFlowUserGroupVM> workFlowUserGroupVMObj = new List<UserXProjectXWorkFlowUserGroupVM>();
            List<UserVM> UserVm = new List<UserVM>();
            if (LevelObj.Sequence != null)
            {
                List<UserModel> UserModel = userBusiness.GetSendto(IDProject.Decrypt(), LevelObj.Sequence, null);


                Mapper.Map(UserModel, UserVm);
            }


            LevelVM levelVM = new LevelVM();
            LevelModel levelModel = levelBusiness.GetDataForFields(userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel.IDWorkFlowUserGroup);
            Mapper.Map(levelModel, levelVM);

            var Fields = GetGIGCreateDynamicFields(IDProject, IDProject, null);


            return Json(new { UserXProjectXWorkFlowUserGroupVM = UserXProjectXWorkFlowUserGroupVM, Fields = Fields, Contractors = contractorVM, WorkFlowUserGroupVM = UserVm, LevelVM = levelVM, GroupName = LevelObj.Name }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUsersallSendTo(string IDUPW, Guid IDGIG)
        {
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM = new WorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            Mapper.Map(userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM);
            Int32? IDContractor = null;

            LevelModel LevelObj = GetNextLevel(userXProjectXWorkFlowUserGroupModel.IDProject, userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, true);

            List<UserXProjectXWorkFlowUserGroupVM> workFlowUserGroupVMObj = new List<UserXProjectXWorkFlowUserGroupVM>();
            List<UserVM> UserVm = new List<UserVM>();
            if (LevelObj.Sequence != null)
            {
                List<UserModel> UserModel = userBusiness.GetSendto(UserXProjectXWorkFlowUserGroupVM.IDProject.Decrypt(), LevelObj.Sequence, null);


                Mapper.Map(UserModel, UserVm);
            }
            return Json(new { WorkFlowUserGroupVM = UserVm }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserForSendTo(string IDUPW, string IDContractor)
        {
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM = new WorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            Mapper.Map(userXProjectXWorkFlowUserGroupModel.workFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM.workFlowUserGroupVM);
            LevelModel LevelObj = GetNextLevel(userXProjectXWorkFlowUserGroupModel.IDProject, userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, true);
            List<UserXProjectXWorkFlowUserGroupVM> workFlowUserGroupVMObj = new List<UserXProjectXWorkFlowUserGroupVM>();
            List<UserVM> UserVm = new List<UserVM>();
            if (LevelObj.Sequence != null)
            {
                List<UserModel> UserModel = userBusiness.GetSendto(UserXProjectXWorkFlowUserGroupVM.IDProject.Decrypt(), LevelObj.Sequence, IDContractor.Decrypt());


                Mapper.Map(UserModel, UserVm);
            }

            return Json(new { WorkFlowUserGroupVM = UserVm }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetGIGCreateDynamicFields(string IDUPW, string IDProject, Nullable<Guid> IDGig)
        {
            List<UDFValidationDefinitionVM> UDFValidationDefinitionVM = new List<UDFValidationDefinitionVM>();
            List<CreateGigVM> CreateGigVM = new List<CreateGigVM>();

            List<CreateGigModel> CreateGigModel = CreateGIGBusiness.GetGIGCreate(IDUPW.Decrypt(), IDProject.Decrypt(), IDGig);
            // List<CreateGigModel> CreateGigModel = CreateGIGBusiness.GetGIGCreate(13, 2, IDGig);

            List<UDFValidationDefinitionModel> UDFValidationDefinitionModel = IUDFValidationDefinitionBusiness.GetGIGValidations();
            Mapper.Map(CreateGigModel, CreateGigVM);
            Mapper.Map(UDFValidationDefinitionModel, UDFValidationDefinitionVM);

            return Json(new { createGigVM = CreateGigVM, UDFValidationDefinitionVM = UDFValidationDefinitionVM }, JsonRequestBehavior.AllowGet);


        }
        public bool SaveLifeCycle(LifeCycleVM lifeCycleVm)
        {
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            Mapper.Map(lifeCycleVm, lifeCycleModel);

            return false;
        }
        public ActionResult SubmitGIGData(GIGVM gIGVM, List<CreateGigVM> ObjUpdated, List<AttachmentVM> attachment, LifeCycleVM lifeCycleVM, string IDWorkflowUserGroup, string IDProject)
        {



            GIGModel gigModel = new GIGModel();
            Mapper.Map(gIGVM, gigModel);
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(gIGVM.IDUPW.Decrypt());
            if (userXProjectXWorkFlowUserGroupModel == null)
            {

                return RedirectToAction("login", "site");
            }

            LevelXFunctionModel levelXFunctionModel = LifeCycleBusiness.GetLevelXFunctionData(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup);
            Int32 IDLevelXFunction = levelXFunctionModel.IDLevelXFunction;



            LevelModel LevelObj = levelBusiness.GetCurrentLevel(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, levelXFunctionModel.sequence);
            if (LevelObj.Sequence == null)
            {

                return Json(new { Success = false, Submit = true, Message = "Next Level Not Available" });

            }

            Guid IDGig = GIGBusiness.SaveGIGData(gigModel);
            if (IDGig == Guid.Empty)
            {

                return Json(new { Success = false, Submit = true, Message = "This GIG Number already exists" });

            }
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            lifeCycleModel.IDLifeCycle = Guid.NewGuid();
            lifeCycleModel.IDGig = IDGig;
            lifeCycleModel.IDLevelXFunction = IDLevelXFunction;
            lifeCycleModel.LoggedByID = (int)SessionManagement.LoggedInUser.IDUser;
            lifeCycleModel.OwnedByID = (int)SessionManagement.LoggedInUser.IDUser;
            lifeCycleModel.AssignedToID = gIGVM.AssignedToID.Decrypt();
            lifeCycleModel.LoggedOn = DateTime.UtcNow;
            lifeCycleModel.ISSubmitted = true;
            lifeCycleModel.Detail = lifeCycleVM.Detail;

            lifeCycleModel.IDLevel = LevelObj.IDLevel;
            Guid IdLifeCycle = lifeCycleBusiness.SaveLifeCycleData(lifeCycleModel);

            List<CreateGigModel> createGigModel = new List<CreateGigModel>();
            Mapper.Map(ObjUpdated, createGigModel);
            Boolean ret = uDFInstanceBusiness.SaveUDfFieldsData(createGigModel, IDGig);

            // Update life cycle>
            LifeCycleModel lifeCycleModelobj = new LifeCycleModel();
            Mapper.Map(lifeCycleVM, lifeCycleModelobj);
            lifeCycleModelobj.IDGig = IDGig;
            lifeCycleModelobj.IDLifeCycle = Guid.NewGuid();
            lifeCycleModelobj.LoggedOn = DateTime.UtcNow;
            lifeCycleModelobj.Detail = null;
            lifeCycleModelobj.LoggedByID = (int)SessionManagement.LoggedInUser.IDUser;
            lifeCycleModelobj.OwnedByID = lifeCycleVM.AssignedToID.Decrypt();
            lifeCycleModelobj.AssignedToID = null;
            lifeCycleModelobj.IDLevelXFunction = null;
            LevelXFunctionModel LevelXFunctionModel = lifeCycleBusiness.GetIScreatedNotCLosed(lifeCycleModelobj.IDLevelXFunction);
            LevelModel LevelObjNextLevel = GetNextLevel(IDProject.Decrypt(), IDWorkflowUserGroup.Decrypt(), true);
            List<UserXProjectXWorkFlowUserGroupVM> workFlowUserGroupVMObj = new List<UserXProjectXWorkFlowUserGroupVM>();

            if (LevelObj.Sequence == null)
            {

                return Json(new { Success = false, Submit = true, Message = "Next Level Not Available" });

            }
            lifeCycleModelobj.IDLevel = LevelObjNextLevel.IDLevel;

            Guid IDLifeCycle = LifeCycleBusiness.SaveLifeCycleData(lifeCycleModelobj);
            List<AttachmentModel> SaveAttachmentModel = new List<AttachmentModel>();
            List<AttachmentModel> DeleteAttachmentModel = new List<AttachmentModel>();
            List<AttachmentModel> attachmentModel = new List<AttachmentModel>();
            if (attachment != null)
            {
                attachment.ToList().ForEach(x => { x.IDLifeCycle = IdLifeCycle; });
                Mapper.Map(attachment, attachmentModel);
                SaveAttachmentModel = attachmentModel.Where(a => a.IDAttachment == 0).ToList();
                DeleteAttachmentModel = attachmentModel.Where(a => a.IDAttachment != 0).ToList();
                Boolean Attechmentret = attachmentBusiness.SaveAttachments(SaveAttachmentModel);
               // Boolean DeleteAttachmentrtn = attachmentBusiness.DeleteAttachments(DeleteAttachmentModel);
            }
            //string message = string.Format("Successfully processed {0} item(s).", model.CartItems.Count.ToString());

            return Json(new { Success = true, Message = "message" });

        }
        /// <summary>
        /// Create GIG and Start with llifecycle log
        /// </summary>
        /// <param name="ObjUpdated"></param>
        /// <param name="IDGIG"></param>
        /// <returns></returns>
        public ActionResult SaveGIGData(GIGVM gIGVM, List<AttachmentVM> attachmentVM, string IDProject)
        {
            GIGModel gigModel = new GIGModel();

            Mapper.Map(gIGVM, gigModel);
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(gIGVM.IDUPW.Decrypt());
            if (userXProjectXWorkFlowUserGroupModel == null)
            {

                return RedirectToAction("login", "site");
            }

            LevelXFunctionModel levelXFunctionModel = LifeCycleBusiness.GetLevelXFunctionData(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup);
            Int32 IDLevelXFunction = levelXFunctionModel.IDLevelXFunction;



            LevelModel LevelObj = levelBusiness.GetIDLevel(levelXFunctionModel.sequence, userXProjectXWorkFlowUserGroupModel.IDProject, true);
            if (LevelObj.Sequence == null)
            {

                return Json(new { Success = false, Submit = true, Message = "Next Level Not Available" });

            }

            Guid IDGig = GIGBusiness.SaveGIGData(gigModel);
            if (IDGig == Guid.Empty)
            {

                return Json(new { Success = false, Submit = true, Message = "This GIG Number already exists" });

            }
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            lifeCycleModel.IDLifeCycle = Guid.NewGuid();
            lifeCycleModel.IDGig = IDGig;
            lifeCycleModel.IDLevelXFunction = IDLevelXFunction;
            lifeCycleModel.LoggedByID = (int)SessionManagement.LoggedInUser.IDUser;
            lifeCycleModel.OwnedByID = (int)SessionManagement.LoggedInUser.IDUser;
            lifeCycleModel.AssignedToID = gIGVM.AssignedToID.Decrypt();
            lifeCycleModel.LoggedOn = DateTime.UtcNow;


            lifeCycleModel.IDLevel = LevelObj.IDLevel;
            Guid IdLifeCycle = lifeCycleBusiness.SaveLifeCycleData(lifeCycleModel);
            List<AttachmentModel> attachmentModel = new List<AttachmentModel>();
            Mapper.Map(attachmentVM, attachmentModel);
            if (attachmentModel.Count > 0)
            {
                attachmentModel.ToList().ForEach(x => { x.IDLifeCycle = IdLifeCycle; });
                bool retAttachment = attachmentBusiness.SaveAttachments(attachmentModel);
            }
            List<AttachmentModel> attachmentModelLst = attachmentBusiness.GetAttachments(IdLifeCycle);
            List<AttachmentVM> attachmentVmlist = new List<AttachmentVM>();
            Mapper.Map(attachmentModelLst, attachmentVmlist);
            var Fields = GetGIGCreateDynamicFields(gIGVM.IDUPW, IDProject, IDGig);
            return Json(new { Submit = false, Success = true, Fields = Fields, Message = "Data Saved Sucessfully", IDGig = IDGig, IDLifeCycle = IdLifeCycle, Attachments = attachmentVmlist });
        }
        [HttpPost]
        public JsonResult UploadDocuments()
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { statusCode = 500, status = "No image provided." });
            }
            var file = Request.Files[0];
            var fileExtension = Path.GetExtension(file.FileName);
            int imageWidth = ReadConfiguration.ProfileImageWidth;
            int imageHeight = ReadConfiguration.ProfileImageHieght;
            string FileName = file.FileName;
            string imageName = fileExtension;
            string MiliSecond = DateTime.Now.Millisecond.ToString();
            string imagePathAndName = "/Uploads/GIGAttechments/" + MiliSecond + "_" + FileName;
            string Location = MiliSecond + "_" + FileName;

            string ImageSavingPath = Server.MapPath(@"~" + imagePathAndName);

            try
            {
                CommonFunctions.UploadFile(file, ImageSavingPath, imageWidth, imageHeight);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    statusCode = 500,
                    status = "Error uploading image.",
                    file = string.Empty
                });
            }
            //userBusiness.ChangeImage(userId, imageName);
            //if (SessionManagement.LoggedInUser.UserId == userId)
            //    SessionManagement.LoggedInUser.ProfileImageUrl = Constants.PROFILE_IMAGE_PATH + imageName + "?" + DateTime.Now.Millisecond;
            //// Return JSON
            return Json(new
            {
                statusCode = 200,
                status = "Image uploaded.",
                Name = FileName,
                Location = Location,
            });

        }
        public JsonResult getFirstLevelActions(string IDUPW, string IDProject, string IDGIG)
        {
            var UserId = SessionManagement.LoggedInUser.IDUser;
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());


            List<FunctionVM> functionVM = new List<FunctionVM>();
            LevelXFunctionVM LevelXFunctionVM = new LevelXFunctionVM();



            LevelXFunctionModel levelXFunctionModel = LifeCycleBusiness.GetLevelXFunctionData(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup);
            // LevelXFunctionModel levelXFunctionModelObj = LifeCycleBusiness.GetLevelXFunctionID(userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup);

            Nullable<bool> IsCreatedNotClosed = null;



            if (levelXFunctionModel.IsCreatedNotClosed == true && levelXFunctionModel.sequence == 1)
            {
                IsCreatedNotClosed = true;

            }

            if (IDGIG != "")
            {
                Guid IDGIg = new Guid(IDGIG);
                LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetLifeCycleData(IDGIg, levelXFunctionModel.IDLevelXFunction);
                if (levelXFunctionModel.IsCreatedNotClosed == false && levelXFunctionModel.sequence == 1)
                {

                    if (lifecyclemodelobjdata.ISSubmitted == true)
                    {

                        IsCreatedNotClosed = false;
                    }
                }


            }

            List<FunctionModel> functionModel = FunctionBusiness.GetFirstLevelActions(UserId, IDProject.Decrypt(), userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, IsCreatedNotClosed);

            Mapper.Map(levelXFunctionModel, LevelXFunctionVM);
            Mapper.Map(functionModel, functionVM);
            return Json(new { FunctionVM = functionVM, levelXFunctionID = LevelXFunctionVM.IDLevelXFunction }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ProccessGIG(Guid IDGIG, string IDLevelXFunction, string IDFunctionTrigger)
        {
            var UserId = SessionManagement.LoggedInUser.IDUser;
            if (UserId == 0)
            {

                return RedirectToAction("login", "site");
            }

            GIGModel GigModel = GetProjectIDUPW(IDGIG);
            GIGVM GIGVM = new GIGVM();
            Mapper.Map(GigModel, GIGVM);
            ViewBag.AnswerDiv = true;
            ViewBag.ShowActionandreceiver = true;

            ViewBag.IDFunctionTrigger = IDFunctionTrigger;
            if (IDLevelXFunction != null)
            {

                ViewBag.AnswerDiv = false;
                LevelXFunctionModel LevelXFunctionModel = lifeCycleBusiness.GetIScreatedNotCLosed(IDLevelXFunction.Decrypt());

                if (LevelXFunctionModel.IsCreatedNotClosed == false)
                {

                    ViewBag.ShowActionandreceiver = 1;
                }
            }
            ViewBag.IDUPW = GIGVM.IDUPW;
            ViewBag.IDProject = GIGVM.UserXProjectXWorkFlowUserGroupModel.IDProject.Encrypt();
            ViewBag.IDWorkFlowUserGroup = GIGVM.UserXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup.Encrypt();
            ViewBag.GIG = IDGIG;
            ViewBag.IDLevelXFunction = IDLevelXFunction;
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(GIGVM.IDUPW.Decrypt());
            if (userXProjectXWorkFlowUserGroupModel == null)
            {

                return RedirectToAction("login", "site");
            }
            ViewBag.Direction = -1;
            LevelXFunctionModel levelXFunctionModel = new LevelXFunctionModel();
            if (IDLevelXFunction != "" && IDLevelXFunction != null && IDLevelXFunction != "0")
            {
                levelXFunctionModel = FunctionBusiness.GetFunctionDetail(IDLevelXFunction.Decrypt());
                ViewBag.Direction = levelXFunctionModel.FunctionModel.Direction ?? 0;
            }

            //List<UserVM> userVm = BindSendTo(levelXFunctionModel.FunctionModel.Direction??0, GIGVM.UserXProjectXWorkFlowUserGroupModel.IDProject.Encrypt(), GIGVM.UserXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup.Encrypt(), IDGIG);
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetActionsRestriction(IDGIG);
            ViewBag.ISDisplayContractor = lifecyclemodelobjdata.LevelModel.ISDisplayContractor;
            ViewBag.ISDisplayContractorGIGNumber = lifecyclemodelobjdata.LevelModel.ISDisplayContractorGIGNumber;


            int? LevelSquence = lifecyclemodelobjdata.LevelModel.Sequence;
            LevelSquence = lifecyclemodelobjdata.LevelModel.Sequence;


            if (ViewBag.Direction != 1)
            {
                LevelModel Level = GetNextLevel(GIGVM.UserXProjectXWorkFlowUserGroupModel.IDProject, userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, false);


            }
            LevelModel LevelObj = levelBusiness.GetIDLevel(userXProjectXWorkFlowUserGroupModel.IDProject, LevelSquence, true);
            if (LevelObj.Sequence == null)
            {
                ViewBag.Leveldata = 0;
            }

            else
            {
                ViewBag.Leveldata = 1;
            }
            return View(UserXProjectXWorkFlowUserGroupVM);

        }
        public JsonResult GetGIGViewData(string IDUPW, string IDProject, Guid IDGIG, string IDFunctionTrigger)
        {
            List<FunctionVM> functionVM = GetActionsForLoggedInUsers(IDProject, IDGIG, IDUPW,IDFunctionTrigger);
            Guid IdLifeCycle = lifeCycleBusiness.GetLifeCycleIdForCreatedGIGByGidId(IDGIG);
            
           
            Int32 userdata = (int)SessionManagement.LoggedInUser.IDUser;
            LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetActionsRestriction(IDGIG);
            LifeCycleModel lifecyclemodelobj = lifeCycleBusiness.GetPreparedByAndOrgainator(IDGIG);
            if (userdata == lifecyclemodelobjdata.AssignedToID)
            {
                functionVM = functionVM.Where(x => x.Enable == true).ToList();

             
            }
            else
            {
                functionVM = null;
            }
            LifeCycleModel lifecyclemodelobjForAttechments = lifeCycleBusiness.GetFIrstLevelCycle(IDGIG);
                List<AttachmentModel> attachmentModelLst = attachmentBusiness.GetAttachments(lifecyclemodelobjForAttechments.IDLifeCycle);
            List<AttachmentVM> attachmentVmlist = new List<AttachmentVM>();
            Mapper.Map(attachmentModelLst, attachmentVmlist);



            var Fields = GetGIGCreateDynamicFields(IDUPW, IDProject, IDGIG);
            UserXProjectXWorkFlowUserGroupModel userXProjectXWorkFlowUserGroupModel = UserXProjectXWorkFlowUserGroupBusiness.GetWOrkFlowProjectDetail(IDUPW.Decrypt());
            UserXProjectXWorkFlowUserGroupVM UserXProjectXWorkFlowUserGroupVM = new UserXProjectXWorkFlowUserGroupVM();
            Mapper.Map(userXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM);
            GIGVM GIGvm = new GIGVM();
            GIGModel GIGModel = GIGBusiness.GetGIG(IDGIG);
            GIGvm.UserXProjectXWorkFlowUserGroupModel = new UserXProjectXWorkFlowUserGroupModel();
            Mapper.Map(GIGModel.UserXProjectXWorkFlowUserGroupModel, GIGvm.UserXProjectXWorkFlowUserGroupModel);
            GIGvm.UserVM = new UserVM();
            LifeCycleVM lifeCycleVM = new LifeCycleVM();
            List<LifeCycleModel> LifeCycle = lifeCycleBusiness.GetallLifeCycleData(IDGIG);
            Mapper.Map(GIGModel.UserModel, GIGvm.UserVM);
            Mapper.Map(GIGModel, GIGvm);
            GIGvm.GIGOriginatorName = lifecyclemodelobj.User.FirstName + " " + lifecyclemodelobj.User.LastName;
            GIGvm.PreparedByName = lifecyclemodelobj.User.FirstName + " " + lifecyclemodelobj.User.LastName;
            Int32? IDContractor = null;
            LevelModel LevelObj = GetNextLevel(IDProject.Decrypt(), userXProjectXWorkFlowUserGroupModel.IDWorkFlowUserGroup, true);
            List<UserXProjectXWorkFlowUserGroupVM> workFlowUserGroupVMObj = new List<UserXProjectXWorkFlowUserGroupVM>();
            List<UserVM> UserVm = new List<UserVM>();

            if (LevelObj.Sequence != null)
            {
                List<UserModel> UserModel = userBusiness.GetSendto(UserXProjectXWorkFlowUserGroupVM.IDProject.Decrypt(), LevelObj.Sequence, null);
                Mapper.Map(UserModel, UserVm);
            }



            return Json(new { Fields = Fields, LifeCycle = LifeCycle, UserXProjectXWorkFlowUserGroupVM = UserXProjectXWorkFlowUserGroupVM, GIGVM = GIGvm, WorkFlowUserGroupVM = workFlowUserGroupVMObj, FunctionVM = functionVM, Attachments = attachmentVmlist }, JsonRequestBehavior.AllowGet);
        }
        public GIGModel GetProjectIDUPW(Guid IDGIG)
        {

            List<GIGVM> CreateGigVM = new List<GIGVM>();
            GIGModel gigModel = GIGBusiness.GetGIGUDPW(IDGIG);
            return gigModel;

        }
        public JsonResult BindSendTo(int ActionType, string IDProject, string IDWorkFlowUserGroup, Guid IDGIG)
        {
            LevelModel LevelObj = new LevelModel();
            LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetActionsRestriction(IDGIG);
            int? LevelSquence = lifecyclemodelobjdata.LevelModel.Sequence;
            string SelectedUserID = null;
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            if (ActionType == 1)
            {
                LevelObj = levelBusiness.GetIDLevel(IDProject.Decrypt(), LevelSquence, true);
            }
            else
            {
                LevelObj = levelBusiness.GetIDLevel(IDProject.Decrypt(), LevelSquence, false);
                lifeCycleModel = lifeCycleBusiness.GetPreviousLifeCycle(IDGIG, LevelObj.IDLevel);
                SelectedUserID = lifeCycleModel.LoggedByID.Encrypt();
            }


            List<UserModel> UserModel = userBusiness.GetSendto(IDProject.Decrypt(), LevelObj.Sequence, null);

            List<UserVM> UserVm = new List<UserVM>();
            Mapper.Map(UserModel, UserVm);

            //return UserVm;
            return Json(new
            {
                UserVm = UserVm,
                SelectedUserID = SelectedUserID
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getGIGAnswerData(Guid IDGIG, string IDLevelXFunction)
        {
            LifeCycleVM lifeCycleVM = new LifeCycleVM();
            LifeCycleModel LifeCycle = lifeCycleBusiness.GetLifeCycleData(IDGIG, IDLevelXFunction.Decrypt());

            if (LifeCycle.IDLifeCycle == Guid.Empty)
            {
                lifeCycleVM = null;

            }
            else
            {
                lifeCycleVM.AttachmentVM = new List<AttachmentVM>();
                Mapper.Map(LifeCycle.AttachmentModel, lifeCycleVM.AttachmentVM);
                Mapper.Map(LifeCycle, lifeCycleVM);
            }

            return Json(new
            {
                LiFeCycle = lifeCycleVM
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveGIGActionData(int? IDAction, string IDWorkFlowUserGroup, List<AttachmentVM> attachment, LifeCycleVM lifeCycleVM, string IDProject)
        {

            //GetLast LifeCycle
            LifeCycleModel lifecyclemodelobjdata = lifeCycleBusiness.GetActionsRestriction(lifeCycleVM.IDGig);
            int? LevelSquence = lifecyclemodelobjdata.LevelModel.Sequence;
           
            Int32 IDLevelXFunction = lifeCycleVM.IDLevelXFunction.Decrypt();
            lifecyclemodelobjdata.LoggedByID = SessionManagement.LoggedInUser.IDUser;

            if (IDAction == 0)
            {
                lifecyclemodelobjdata.OwnedByID = SessionManagement.LoggedInUser.IDUser;
                lifecyclemodelobjdata.AssignedToID = null;
            }
            else
            {
              //lifecyclemodelobjdata.OwnedByID = lifeCycleVM.OwnedByID.Decrypt();
                lifecyclemodelobjdata.AssignedToID = lifeCycleVM.AssignedToID.Decrypt();
            }
            lifecyclemodelobjdata.Detail = lifeCycleVM.Detail;
            lifecyclemodelobjdata.IDLevelXFunction = lifeCycleVM.IDLevelXFunction.Decrypt();
            lifecyclemodelobjdata.LoggedOn = DateTime.UtcNow;
            Guid lifeCycle = LifeCycleBusiness.SaveLifeCycleData(lifecyclemodelobjdata);
            LevelModel LevelObj = new LevelModel();
            if (IDAction == 1)
            {
                LevelObj = levelBusiness.GetIDLevel(IDProject.Decrypt(), lifecyclemodelobjdata.LevelModel.Sequence, true);
            }
            else
            {
                LevelObj = levelBusiness.GetIDLevel(IDProject.Decrypt(), lifecyclemodelobjdata.LevelModel.Sequence, false);
            }
            LevelXFunctionModel LevelXFunctionModel = lifeCycleBusiness.GetIScreatedNotCLosed(lifeCycleVM.IDLevelXFunction.Decrypt());
            if (LevelObj.Sequence == null && LevelXFunctionModel.IsCreatedNotClosed != false)
            {
                return Json(new { Success = false, Message = "Next Level Not Available" });
            }

            LifeCycleModel lifeCycleModelNew = new LifeCycleModel();
            lifeCycleModelNew.LoggedByID = SessionManagement.LoggedInUser.IDUser;
            lifeCycleModelNew.OwnedByID = lifecyclemodelobjdata.AssignedToID;
            lifeCycleModelNew.AssignedToID = null;
                //need to add null value for IDLevelXFunction;
                if (IDAction == 0)      
                {
                    lifeCycleModelNew.IDLevel = lifecyclemodelobjdata.IDLevel;
                    lifeCycleModelNew.OwnedByID = SessionManagement.LoggedInUser.IDUser;
                    lifeCycleModelNew.AssignedToID = null;
                    lifeCycleModelNew.IDLevelXFunction = null;
                    lifeCycleModelNew.Detail = lifecyclemodelobjdata.Detail;
                }
                else
                {
                    lifeCycleModelNew.IDLevelXFunction = null;
                    lifeCycleModelNew.IDLevel = LevelObj.IDLevel;
                }
                lifeCycleModelNew.LoggedOn = DateTime.UtcNow;
                lifeCycleModelNew.IDLifeCycle = Guid.NewGuid();
               
                lifeCycleModelNew.IDGig = lifeCycleVM.IDGig;
                lifeCycleModelNew.ISSubmitted = true;
                Guid lifeCycleID = LifeCycleBusiness.SaveLifeCycleData(lifeCycleModelNew);
         
            // Update life cycle>
            LifeCycleModel lifeCycleModel = new LifeCycleModel();
            lifeCycleVM.LoggedOn = DateTime.UtcNow;
            if (attachment != null)
            {
                attachment.ForEach(x => { x.IDLifeCycle = lifeCycle; });
                List<AttachmentModel> AttachmentModel = new List<AttachmentModel>();
                Mapper.Map(attachment, AttachmentModel);
                Boolean Attechmentret = attachmentBusiness.SaveAttachments(AttachmentModel);
            }
            return Json(new { Success = true, Message = "message", IdLifeCycle = lifeCycle });
        }
        public LevelModel GetNextLevel(int IDproject, int IDWorkFlowUserGroup, bool IsNextNotPreviousLevel)
        {
            LevelXFunctionModel levelXFunctionModel = LifeCycleBusiness.GetLevelXFunctionData(IDWorkFlowUserGroup);
            Int32 IDLevelXFunction = levelXFunctionModel.IDLevelXFunction;

            //  int LevelSquence = levelXFunctionModel.sequence + 1;
            // LevelModel LevelObj = levelBusiness.GetIDLevel(LevelSquence, IDWorkFlowUserGroup);

            LevelModel LevelModel = levelBusiness.GetIDLevel(IDproject, levelXFunctionModel.sequence, IsNextNotPreviousLevel);

            return LevelModel;
        }
    }
}