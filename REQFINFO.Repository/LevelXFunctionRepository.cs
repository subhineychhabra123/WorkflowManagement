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
    public class LevelXFunctionRepository : BaseRepository<LevelXFunction>
    {
        GIGEntities entities;
        public LevelXFunctionRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }


       
    }
}