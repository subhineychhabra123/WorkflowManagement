using REQFINFO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business.Interfaces
{
    public interface ICustomLookupBusiness
    {

        List<CustomLookupModel> GetAllCustomLookUp(int CustomLookUpId);

    }
}
