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
    public class UDFInstanceBusiness : UDFInstanceRepository, IUDFInstanceBusiness
    {
        private readonly UDFInstanceRepository UDFInstanceRepository;
        private readonly CustomLookupBusiness customLookupBusiness;
        public UDFInstanceBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            UDFInstanceRepository = this;
            customLookupBusiness = new CustomLookupBusiness(_unitOfWork);
        }

        public bool SaveUDfFieldsData(List<CreateGigModel> UDFInstanceModel, Guid IDGig)
        {
            List<UDFInstanceModel> udfInstanceModelList = new List<UDFInstanceModel>();

            foreach (CreateGigModel createGIGModelObj in UDFInstanceModel)
            {
                UDFInstance uDFInstance = UDFInstanceRepository.SingleOrDefault(x => x.IDUDFInstance == createGIGModelObj.IDUDFInstance && x.IDGig== IDGig);
                if (uDFInstance == null)
                {
                    UDFInstance uDFInstanceObj = new UDFInstance();
                    UDFInstanceModel objUDFInstanceModel = new UDFInstanceModel();
                    objUDFInstanceModel.IDGig = IDGig;
                    objUDFInstanceModel.IDUDFDefinition = createGIGModelObj.IDUDFDefinition;
                    objUDFInstanceModel.IDUDFInstance = Guid.NewGuid();
                    objUDFInstanceModel.Val1 = createGIGModelObj.Val1;
                    Mapper.Map(objUDFInstanceModel, uDFInstanceObj);
                    UDFInstanceRepository.Insert(uDFInstanceObj);

                }
                else
                {
                    UDFInstanceModel objUDFInstanceModel = new UDFInstanceModel();
                    objUDFInstanceModel.IDGig = uDFInstance.IDGig;
                    objUDFInstanceModel.IDUDFDefinition = uDFInstance.IDUDFDefinition;
                    objUDFInstanceModel.IDUDFInstance = createGIGModelObj.IDUDFInstance;
                    objUDFInstanceModel.Val1 = createGIGModelObj.Val1;
                    Mapper.Map(objUDFInstanceModel, uDFInstance);

                    UDFInstanceRepository.Update(uDFInstance);
                }


                //  udfInstanceModelList.Add(objUDFInstanceModel);
            }

            return true;
        }

    }
}
