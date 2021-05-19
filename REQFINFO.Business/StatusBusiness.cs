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
   public class StatusBusiness : StatusRepository, IStatusBusiness
    {
      #region property
       private readonly StatusRepository StatusRepository;
        #endregion
        public StatusBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            StatusRepository = this;
        
        }
        public List<StatusModel> GetAllStatus()
        {
            List<Tab> status = StatusRepository.GetAll().OrderBy(x => x.Sequence).ToList();
            List<StatusModel> statusModel = new List<StatusModel>();
            Mapper.Map(status, statusModel);
            return statusModel;
        }
      
    }
}
