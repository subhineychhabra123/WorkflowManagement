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
    public class UDFInstanceRepository : BaseRepository<UDFInstance>
    {
        GIGEntities entities;

        public UDFInstanceRepository(IUnitOfWork unit)
              : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }
       

    }
    

}
