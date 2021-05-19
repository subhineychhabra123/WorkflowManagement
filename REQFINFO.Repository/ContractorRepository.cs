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
  public  class ContractorRepository : BaseRepository<Contractor>
    {
        GIGEntities entities;

        public ContractorRepository(IUnitOfWork unit)
              : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }


    }


}
