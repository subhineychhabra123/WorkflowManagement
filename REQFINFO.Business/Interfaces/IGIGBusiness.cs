
using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Repository.Infrastructure.Contract;

namespace REQFINFO.Business.Interfaces
{
    public interface IGIGBusiness
    {
        List<GIGModel> GIGCommunicationLog(Int32 IDUPW, Int32 IDStatus, int PageNumber, string Search, string SortParameter, bool SortOrder, Int32 pageSize, string Searchby, ref int totalRecord);
        GIGModel GetGIGUDPW(Guid IDGIG);
        Guid SaveGIGData(GIGModel gigModel);
        GIGModel GetGIG(Guid IDGIG);
    }
}
