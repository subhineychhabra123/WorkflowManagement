using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REQFINFO.Repository
{
    public class UDFValidationRepository : BaseRepository<UDFValidations>
    {
      GIGEntities entities;
      public UDFValidationRepository(IUnitOfWork unit)
              : base(unit)
          {
              entities = (GIGEntities)this.UnitOfWork.Db;
          }

      public List<RFI_GetUDFFieldsValidations_Result> GetValidations(Int32 IDUDFDefinition)
      {
          List<RFI_GetUDFFieldsValidations_Result> UDFFieldsValidations = entities.RFI_GetUDFFieldsValidations(IDUDFDefinition).ToList();
          return UDFFieldsValidations;
      }

    
    }
}

