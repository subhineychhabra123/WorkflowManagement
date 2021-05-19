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
    public class GIGRepository :BaseRepository<GIG>
    {
        GIGEntities entities;
        public GIGRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;

        }
        public List<RFI_GetGIGCommunicationLog_Result> GetommunicationLogList(Int32 IDUPW, Int32 IDStatus, int PageNumber, string Search, string SortParameter, bool SortOrder, string SearchbyField)
        {

            List<RFI_GetGIGCommunicationLog_Result> UserWorkflowList = new List<RFI_GetGIGCommunicationLog_Result>();
            string dateString = Search;
            string format = "mm-dd-yyyy";
            DateTime dateTime;
           
           if (SearchbyField == "DateCreated" || SearchbyField == "DateRequired")
           {


            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out dateTime))
            {
              
            }
            else
            {
                return UserWorkflowList;
            }
           }
            UserWorkflowList = entities.RFI_GetGIGCommunicationLog(IDUPW, IDStatus, Search, SortParameter, SortOrder, SearchbyField).ToList();
            return UserWorkflowList;
        }
    }
}
