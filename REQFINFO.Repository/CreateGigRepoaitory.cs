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
  public  class CreateGigRepoaitory : BaseRepository<CreateGig>
    {
        GIGEntities entities;
        public CreateGigRepoaitory(IUnitOfWork unit)
              : base(unit)
          {
            entities = (GIGEntities)this.UnitOfWork.Db;
         }

        public List<RFI_GetUDFFieldsData_Result> GetGIGCreate(Int32 IDUPW, Int32 IDProject, Nullable<Guid> IDGIG)
        {
            List<RFI_GetUDFFieldsData_Result> UDFFieldsList = entities.RFI_GetUDFFieldsData(IDUPW, IDProject, IDGIG).ToList();
            return UDFFieldsList;
        }


    }
}

