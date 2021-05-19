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
    public class AttachmentRepository : BaseRepository<Attachment>
    {
        GIGEntities entities;
        public AttachmentRepository(IUnitOfWork unit)
              : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }
    }
}

