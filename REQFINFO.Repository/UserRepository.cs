using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        GIGEntities entities;
        public UserRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }



        public List<RFI_GetSendto_Result> GetSendto(int IDProject, int? sequence, int? IDContractor)
        {

            List<RFI_GetSendto_Result> SendTO = new List<RFI_GetSendto_Result>();

            SendTO = entities.RFI_GetSendto(sequence, IDProject, IDContractor).ToList();
            return SendTO;
        }

    }
}
