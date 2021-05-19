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
    public class ContractorBusiness : ContractorRepository, IContractorBusiness
    {
        private readonly ContractorRepository contractorRepository;
        public ContractorBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            contractorRepository = this;
        }
        public List<ContractorModel> GetContractors()
        {
            List<ContractorModel> contractorModel = new List<ContractorModel>();
            List<Contractor> contractor = contractorRepository.GetAll().ToList();
            Mapper.Map(contractor, contractorModel);
            return contractorModel;
        }

    }
}
