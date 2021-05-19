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
  public  class StatusRepository : BaseRepository<Tab>
    {
      public StatusRepository(IUnitOfWork unit)
              : base(unit)
          {

          }
    }
}

