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
    public class UDFValidationDefinitionBusiness : UDFValidationDefinitionRepoaitory, IUDFValidationDefinitionBusiness
    {


        #region property
        private readonly UDFValidationDefinitionRepoaitory UDFValidationDefinitionRepoaitory;
      

        #endregion
        public UDFValidationDefinitionBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            UDFValidationDefinitionRepoaitory = this;
          
        }


        public List<UDFValidationDefinitionModel> GetGIGValidations()
        {
            List<UDFValidationDefinition> UDFValidationDefinition = UDFValidationDefinitionRepoaitory.GetAll().OrderBy(x => x.IDUDFValidationDefinition).ToList();
            List<UDFValidationDefinitionModel> UDFValidationDefinitionModel = new List<UDFValidationDefinitionModel>();
            Mapper.Map(UDFValidationDefinition, UDFValidationDefinitionModel);

            return UDFValidationDefinitionModel;
        }




    }
}
