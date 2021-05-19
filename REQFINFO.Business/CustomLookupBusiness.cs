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
    public class CustomLookupBusiness : CustumLookupRepository, ICustomLookupBusiness
    {


        #region property
        private readonly CustumLookupRepository CustumLookupRepository;


        #endregion
        public CustomLookupBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            CustumLookupRepository = this;
        }



        public List<CustomLookupModel> GetAllCustomLookUp(int CustomLookUpId)
        {
            List<CustomLookup> customLookUp = new List<CustomLookup>();
            List<CustomLookupModel> customLookUpModel=new List<CustomLookupModel>();
            customLookUp = CustumLookupRepository.GetAll(x => x.IDLookupType == CustomLookUpId).ToList();
            Mapper.Map(customLookUp, customLookUpModel);
            return customLookUpModel;
        }



    }
}
