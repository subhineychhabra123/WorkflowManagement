using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REQFINFO.Repository
{
  public  class CustumLookupRepository : BaseRepository<CustomLookup>
    {
      GIGEntities entities;
      public CustumLookupRepository(IUnitOfWork unit)
              : base(unit)
          {
              entities = (GIGEntities)this.UnitOfWork.Db;
          }    
    }
}

