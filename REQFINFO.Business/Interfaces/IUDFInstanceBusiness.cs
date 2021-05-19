using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Domain;

namespace REQFINFO.Business.Interfaces
{
    public interface IUDFInstanceBusiness
    {
        bool SaveUDfFieldsData(List<CreateGigModel> createGigModel, Guid IDGig);
        //bool SaveUDFInstanceData(List<UDFInstanceModel> uDFInstanceModel);
    }
}
