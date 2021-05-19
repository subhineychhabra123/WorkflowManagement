using ExpressMapper;
using REQFINFO.Business.Interfaces;
using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business
{
    public class CreateGIGBusiness : CreateGigRepoaitory,ICreateGIGBusiness
    {


        #region property
        private readonly CreateGigRepoaitory CreateGigRepoaitory;
        private readonly CustomLookupBusiness customLookupBusiness;
        private readonly UDFValidationBusiness UDFValidationBusiness;

        #endregion
        public CreateGIGBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            CreateGigRepoaitory = this;
            customLookupBusiness = new CustomLookupBusiness(_unitOfWork);


            UDFValidationBusiness = new UDFValidationBusiness(_unitOfWork);
        }


        public List<CreateGigModel> GetGIGCreate(Int32 IDUPW, Int32 IDProject, Nullable<Guid> IDGIG)
        {

            List<CreateGigModel> CreateGigModel = new List<CreateGigModel>();
            List<RFI_GetUDFFieldsData_Result> UDFFieldsList = CreateGigRepoaitory.GetGIGCreate(IDUPW, IDProject,IDGIG);
            Mapper.Map(UDFFieldsList, CreateGigModel);
            foreach (var item in CreateGigModel)
            {
                if (item.IDLookupType!=null)
                {
                    item.CustomLookupModel = new List<CustomLookupModel>();
                    item.CustomLookupModel = customLookupBusiness.GetAllCustomLookUp(item.IDLookupType);

                    item.UDFFieldsValidations = new List<UDFFieldsValidationsModel>();
                    item.UDFFieldsValidations = UDFValidationBusiness.GetValidations(Convert.ToInt32( item.IDUDFDefinition));

                }

            }



            return CreateGigModel;

        }




    }
}
