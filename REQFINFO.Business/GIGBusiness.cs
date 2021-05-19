using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Business.Interfaces;
using System.Collections.Generic;
using ExpressMapper;
using REQFINFO.Utility;
using System.Linq;
using System;

namespace REQFINFO.Business
{
   public class GIGBusiness : GIGRepository, IGIGBusiness
    {
      #region property
       private readonly GIGRepository GIGCommunicationLogsRepository;
        #endregion
        public GIGBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            GIGCommunicationLogsRepository = this;
        
        }
        public List<GIGModel> GIGCommunicationLog(Int32 IDUPW, Int32 IDStatus, int PageNumber, string Search,string SortParameter,bool SortOrder,Int32 pageSize,string Searchby, ref int totalRecord)
        {
            List<GIGModel> GIGCommunicationLogModel = new List<GIGModel>();
            List<RFI_GetGIGCommunicationLog_Result> GetGIGCommunicationLogList = GIGCommunicationLogsRepository.GetommunicationLogList(IDUPW, IDStatus, PageNumber, Search, SortParameter, SortOrder, Searchby);
                                                                                                           

            totalRecord = GetGIGCommunicationLogList.Count();
            GetGIGCommunicationLogList = GetGIGCommunicationLogList.Skip((PageNumber - 1) * pageSize).Take(pageSize).ToList(); ;
            Mapper.Map(GetGIGCommunicationLogList, GIGCommunicationLogModel);
            return GIGCommunicationLogModel;
        }


        public GIGModel GetGIGUDPW(Guid IDGIG)
        {
            GIGModel  gIGModel = new GIGModel();
            GIG GIG = GIGCommunicationLogsRepository.SingleOrDefault(x => x.IDGig == IDGIG );
            gIGModel.UserXProjectXWorkFlowUserGroupModel  = new UserXProjectXWorkFlowUserGroupModel();
            Mapper.Map(GIG, gIGModel);
            Mapper.Map(GIG.UserXProjectXWorkFlowUserGroup, gIGModel.UserXProjectXWorkFlowUserGroupModel);

            return gIGModel;
        }
        public Guid  SaveGIGData(GIGModel gigModel)
        {
            Guid idGig = new Guid();
            GIG gig = new GIG();
            Mapper.Map(gigModel, gig);
            Int32 CompanyID= (int)SessionManagement.LoggedInUser.IDCompany;
            if (gigModel.IDGig == new Guid())
            {
               bool ISGIGExist = GIGCommunicationLogsRepository.Exists(x => x.Number==gigModel.Number  && x.UserXProjectXWorkFlowUserGroup.User.Company.IDCompany == CompanyID);
                if(ISGIGExist)
                {
                    return Guid.Empty;
                }
                else
                {
                gig.IDGig = Guid.NewGuid();
                GIGCommunicationLogsRepository.Insert(gig);
                idGig = gig.IDGig;
                }
            }
            else
            {
                gig = GIGCommunicationLogsRepository.SingleOrDefault(x => x.IDGig == gigModel.IDGig);
                if (gig != null)
                {
                    Mapper.Map(gigModel, gig);
                    GIGCommunicationLogsRepository.Update(gig);
                    idGig = gig.IDGig;
                }
            }


            return idGig;
        }

        public GIGModel GetGIG(Guid IDGIG)
        {
            GIG gig = GIGCommunicationLogsRepository.SingleOrDefault(x => x.IDGig== IDGIG);
            GIGModel gigModel = new GIGModel();
            gigModel.UserXProjectXWorkFlowUserGroupModel = new UserXProjectXWorkFlowUserGroupModel();
            Mapper.Map(gig.UserXProjectXWorkFlowUserGroup, gigModel.UserXProjectXWorkFlowUserGroupModel);
            
            

            gigModel.UserModel = new UserModel();
            
          
            Mapper.Map(gig, gigModel);
            return gigModel;
        }

    }
}
