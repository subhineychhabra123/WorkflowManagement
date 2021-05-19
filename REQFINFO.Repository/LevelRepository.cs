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
    public class LevelRepository : BaseRepository<Level>
    {
        GIGEntities entities;
        public LevelRepository(IUnitOfWork unit)
              : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }

        public GetLevelDetailNextOrPrevious_Result GetIDLevel(Int32 IDProject, Int32? CurrentSequence, bool IsNextNotPreviousLevel)
        {

            GetLevelDetailNextOrPrevious_Result LevelDetailNextOrPrevious = new GetLevelDetailNextOrPrevious_Result();

            LevelDetailNextOrPrevious = entities.GetLevelDetailNextOrPrevious(IDProject, CurrentSequence, IsNextNotPreviousLevel).SingleOrDefault();
            return LevelDetailNextOrPrevious;
        }

    }
}

