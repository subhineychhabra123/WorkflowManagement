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
    public class UDFValidationBusiness : UDFValidationRepository, IUDFValidationBusiness
    {


        #region property
        private readonly UDFValidationRepository UDFValidationRepository;


        #endregion
        public UDFValidationBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            UDFValidationRepository = this;
        }



        public List<UDFFieldsValidationsModel> GetValidations(Int32 IDUDFDefinition)
        {
          
            List<UDFFieldsValidationsModel> UDFFieldsValidationsModel = new List<UDFFieldsValidationsModel>();
            List<RFI_GetUDFFieldsValidations_Result> UDFFieldsValidations = UDFValidationRepository.GetValidations(IDUDFDefinition);

            Mapper.Map(UDFFieldsValidations, UDFFieldsValidationsModel);
            return UDFFieldsValidationsModel;
        }



    }
}
