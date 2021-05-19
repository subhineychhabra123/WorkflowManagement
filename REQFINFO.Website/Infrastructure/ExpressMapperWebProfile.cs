using REQFINFO.Business.Infrastructure;
using REQFINFO.Website;
using REQFINFO.Website.Models;
using REQFINFO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REQFINFO.Utility;
using ExpressMapper;
namespace REQFINFO.Website.Infrastructure
{
    public class ExpressMapperWebProfile
    {

        public static void RegisterMapping()
        {
            #region ModeltoDomain Model
            //Mapper.Register<LifeCycleVM, LifeCycleModel>();
            Mapper.Register<LifeCycleVM, LifeCycleModel>()
                .Member(dest => dest.IDLevelXFunction, src => !string.IsNullOrEmpty(src.IDLevelXFunction) ? src.IDLevelXFunction.Decrypt() : Convert.ToInt32(src.IDLevelXFunction))
                  .Member(dest => dest.AssignedToID, src => !string.IsNullOrEmpty(src.AssignedToID) ? src.AssignedToID.Decrypt() : Convert.ToInt32(src.AssignedToID))
                   .Member(dest => dest.IDLevel, src => !string.IsNullOrEmpty(src.IDLevel) ? src.IDLevel.Decrypt() : Convert.ToInt32(src.IDLevel))
                .Member(dest => dest.OwnedByID, scr => scr.OwnedByID.Decrypt())
                 .Member(dest => dest.IDLevel, scr => scr.IDLevel.Decrypt())
                .Member(dest => dest.LoggedByID, scr => scr.LoggedByID.Decrypt());

                //.Member(dest => dest.own, src => src.IDLevelXFunction.Decrypt()); 
            Mapper.Register<UserVM, UserModel>().Member(dest => dest.IDUser, src => src.IDUser.Decrypt());
            Mapper.Register<WorkFLowVM, WorkFlowModel>().Member(dest => dest.IDWorkflow, src => src.IDWorkflow.Decrypt());
            Mapper.Register<ProjectVM, ProjectModel>().Member(dest => dest.IDUPW, src => src.IDUPW.Decrypt());

            Mapper.Register<FunctionVM, FunctionModel>().Member(dest => dest.IDFunction, src => src.IDFunction.Decrypt())
                .Member(dest => dest.IDLevelXFunction, src => src.IDLevelXFunction.Decrypt())
                .Member(dest => dest.IDTab, src => src.IDTab.Decrypt());
            Mapper.Register<UserXProjectXWorkFlowUserGroupVM, UserXProjectXWorkFlowUserGroupModel>().Member(dest => dest.IDProject, src => src.IDProject.Decrypt()).Member(dest => dest.IDUPW, src => src.IDUPW.Decrypt()).Member(dest => dest.IDWorkFlowUserGroup, src => src.IDWorkFlowUserGroup.Decrypt()).Member(dest => dest.IDUser, src => src.IDUser.Decrypt());
            Mapper.Register<CreateGigVM, CreateGigModel>();
            Mapper.Register<GIGVM, GIGModel>().Member(dest => dest.IDUPW, src => src.IDUPW.Decrypt())
                .Member(dest =>  dest.IDFunctionTrigger,src =>src.IDFunctionTrigger.Decrypt())
                // .Member(dest => dest.GIGOriginator, src => src.GIGOriginator.Decrypt())
                //.Member(dest => dest.PreparedBy, src => src.PreparedBy.Decrypt())
                // .Member(dest => dest.SendTo, src => src.SendTo.Decrypt())
                //     .Member(dest => dest.IDContractor, src => src.IDContractor.Decrypt())

                 .Member(dest => dest.AssignedToID, src => !string.IsNullOrEmpty(src.AssignedToID) ? src.AssignedToID.Decrypt() : Convert.ToInt32(src.AssignedToID))
                 .Member(dest => dest.GIGOriginator, src => !string.IsNullOrEmpty(src.GIGOriginator) ? src.GIGOriginator.Decrypt() : Convert.ToInt32(src.GIGOriginator))
                  .Member(dest => dest.PreparedBy, src => !string.IsNullOrEmpty(src.PreparedBy)? src.PreparedBy.Decrypt() : Convert.ToInt32(src.PreparedBy))
                .Member(dest => dest.SendTo, src => !string.IsNullOrEmpty(src.SendTo) ? src.SendTo.Decrypt() : Convert.ToInt32(src.SendTo))
                      .Member(dest => dest.IDContractor, src => !string.IsNullOrEmpty(src.IDContractor) ? src.IDContractor.Decrypt() : Convert.ToInt32(src.IDContractor));
                     
            Mapper.Register<CustomLookupVM, CustomLookupModel>();
            Mapper.Register<UDFFieldsValidationsVM, UDFFieldsValidationsModel>();
            Mapper.Register<UDFValidationDefinitionVM, UDFValidationDefinitionModel>();
            Mapper.Register<AttachmentVM, AttachmentModel>();
            Mapper.Register<WorkFlowUserGroupVM, WorkFlowUserGroupModel>().Member(dest => dest.IDWorkFlow, src => src.IDWorkFlow.Decrypt())
                .Member(dest => dest.IDWorkFlowUserGroup, src => src.IDWorkFlowUserGroup.Decrypt());
            Mapper.Register<ContractorVM, ContractorModel>().Member(dest => dest.IDContractor, src => src.IDContractor.Decrypt());
            Mapper.Register<LevelVM, LevelModel>().Member(dest => dest.IDLevel, src => src.IDLevel.Decrypt()).Member(dest => dest.IDWorkFlowUserGroup, src => src.IDWorkFlowUserGroup.Decrypt());
            Mapper.Register<LevelXFunctionVM, LevelXFunctionModel>().Member(dest => dest.IDLevelXFunction, src => src.IDLevelXFunction.Decrypt());
            #endregion


            #region  DomainModeltoModel
            Mapper.Register<UserModel, UserVM>().Member(dest => dest.IDUser, src => src.IDUser.Encrypt());
            Mapper.Register<WorkFlowModel, WorkFLowVM>().Member(dest => dest.IDWorkflow, src => src.IDWorkflow.Encrypt()); ;
            Mapper.Register<ProjectModel, ProjectVM>().Member(dest => dest.IDUPW, src => src.IDUPW.Encrypt()).Member(dest => dest.IDProject, src => src.IDProject.Encrypt());
            Mapper.Register<FunctionModel, FunctionVM>().Member(dest => dest.IDFunction, src => src.IDFunction.Encrypt())
                .Member(dest => dest.IDLevelXFunction, src => src.IDLevelXFunction.Encrypt())
                .Member(dest => dest.IDTab, src => src.IDTab.Encrypt());
            Mapper.Register<UserXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroupVM>().Member(dest => dest.IDProject, src => src.IDProject.Encrypt()).Member(dest => dest.IDUPW, src => src.IDUPW.Encrypt()).Member(dest => dest.IDWorkFlowUserGroup, src => src.IDWorkFlowUserGroup.Encrypt()).Member(dest => dest.IDUser, src => src.IDUser.Encrypt());
            Mapper.Register<CustomLookupVM, CustomLookupVM>();
            Mapper.Register<CreateGigModel, CreateGigVM>()
                .After((s, d) =>
                {
                    if (s.Val1 == null)
                        d.Val1 = s.DefaultValue;
                   

                });
               
            Mapper.Register<UDFFieldsValidationsModel, UDFFieldsValidationsVM>();
            Mapper.Register<UDFValidationDefinitionModel, UDFValidationDefinitionVM>();
            Mapper.Register<AttachmentModel, AttachmentVM>();
            Mapper.Register<ContractorModel, ContractorVM>().Member(dest => dest.IDContractor, src => src.IDContractor.Encrypt());
            Mapper.Register<GIGModel, GIGVM>()
                .Member(dest => dest.IDContractor, src => src.IDContractor.Encrypt())
                .Member(dest => dest.IDUPW, src => src.IDUPW.Encrypt())
                .Member(dest => dest.GIGOriginator, src => src.GIGOriginator.Encrypt())
                .Member(dest => dest.PreparedBy, src => src.PreparedBy.Encrypt())
                .Member(dest => dest.IDFunctionTrigger, src => src.IDFunctionTrigger.Encrypt())
                .Member(dest => dest.SendTo, src => src.SendTo.Encrypt());

            Mapper.Register<WorkFlowUserGroupModel, WorkFlowUserGroupVM>().Member(dest => dest.IDWorkFlow, src => src.IDWorkFlow.Encrypt());
            Mapper.Register<LifeCycleModel, LifeCycleVM>()
                .Member(dest => dest.IDLevelXFunction, src =>  src.IDLevelXFunction !=null? src.IDLevelXFunction.Encrypt() :null)
                    .Member(dest => dest.AssignedToID, scr => scr.AssignedToID.Encrypt())
                .Member(dest => dest.OwnedByID, scr => scr.OwnedByID.Encrypt())
                .Member(dest => dest.IDLevel, scr => scr.IDLevel.Encrypt())
                .Member(dest => dest.LoggedByID, scr => scr.LoggedByID.Encrypt())
                
                ;
            Mapper.Register<LevelXFunctionModel,LevelXFunctionVM>().Member(dest => dest.IDLevelXFunction, src => src.IDLevelXFunction.Encrypt());
            Mapper.Register<LevelModel, LevelVM>().Member(dest => dest.IDLevel, src => src.IDLevel.Encrypt()).Member(dest => dest.IDWorkFlowUserGroup, src => src.IDWorkFlowUserGroup.Encrypt());

            #endregion
        }
    }




}