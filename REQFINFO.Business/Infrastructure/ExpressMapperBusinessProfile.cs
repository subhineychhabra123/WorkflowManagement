using REQFINFO.Business;
using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Utility;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business.Infrastructure
{
    public class ExpressMapperBusinessProfile
    {
        public static void RegisterMapping()
        {
            #region business to entity


            Mapper.Register<UserModel, User>();
            Mapper.Register<WorkFlowModel, RFI_GetUserWorkflow_Result>();
            Mapper.Register<FunctionModel, Function>();
            Mapper.Register<CustomLookupModel,CustomLookup>();
            Mapper.Register<LifeCycleModel,LifeCycle>()
                .Member(dest => dest.IDLevel, src => src.IDLevel > 0 ? src.IDLevel : (int?)null)
                .Member(dest => dest.AssignedToID, src => src.AssignedToID > 0 ? src.AssignedToID : (int?)null); 

            Mapper.Register<StatusModel, Tab>();
            Mapper.Register<UserXProjectXWorkFlowUserGroupModel, UserXProjectXWorkFlowUserGroup>();
            Mapper.Register<UDFInstanceModel, UDFInstance>();
            Mapper.Register<WorkFlowUserGroupModel, WorkFlowUserGroup>();
            Mapper.Register<GIGModel, GIG>()
                //.Member(dest => dest.Originator, src => src.GIGOriginator)
                // .Member(dest => dest.SendTo, src => src.SendTo > 0 ? src.SendTo : (int?)null)
                .Member(dest => dest.IDContractor, src => src.IDContractor > 0 ? src.IDContractor : (int?)null);
                

                           
            #endregion



            #region  entity to business
            Mapper.Register<User, UserModel>();
            Mapper.Register<Tab, StatusModel>();
            Mapper.Register<Function, FunctionModel>();
            Mapper.Register<UDFInstance, UDFInstanceModel>();
            Mapper.Register<RFI_GetUserWorkflow_Result, WorkFlowModel>();
            Mapper.Register<RFI_GetActionsForLoggedInUser_Result, FunctionModel>();
            Mapper.Register<GetLevelDetailNextOrPrevious_Result, LevelModel>();
            Mapper.Register<RFI_GetSendto_Result, UserModel>();
            Mapper.Register<RFI_GetUDFFieldsData_Result, CreateGigModel>();
            Mapper.Register<CustomLookup, CustomLookupModel>();
            Mapper.Register<WorkFlowUserGroup, WorkFlowUserGroupModel>();
            Mapper.Register<LifeCycle, LifeCycleModel>();
            

            Mapper.Register<GIG, GIGModel>()

               // .Member(dest => dest.GIGOriginator, src => src.Originator)
               // .Member(dest => dest.GIGOriginatorName, src => src.User.FirstName+' '+ src.User.LastName)
                //.Member(dest => dest.PreparedByName, src => src.User.FirstName + ' ' + src.User.LastName)
              .Member(dest => dest.ContractorName, src => src.Contractor.Name);
            Mapper.Register<UserXProjectXWorkFlowUserGroup, UserXProjectXWorkFlowUserGroupModel>()
                .Member(dest => dest.WorkflowName, src => src.WorkFlowUserGroup.Workflow.Name)
                .Member(dest => dest.ProjectName, src => src.Project.Name).Member(dest => dest.WorkflowDisplayName, src => src.WorkFlowUserGroup.Workflow.DisplayName)
                .Member(dest => dest.IDWorkFlow, src => src.WorkFlowUserGroup.Workflow.IDWorkflow)
                    .Member(dest => dest.UserName, src => src.User.FirstName + ' ' + src.User.LastName);
            Mapper.Register<RFI_GetGIGCommunicationLog_Result, GIGModel>()
               .Member(dest => dest.GIGDirection, src => src.TriggerValue);
            #endregion


        }
    }

}
