using REQFINFO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business.Interfaces
{
   public interface ICreateGIGBusiness
    {

       List<CreateGigModel> GetGIGCreate(Int32 IDUPW, Int32 IDProject, Nullable<Guid> IDGIG);

    }
}
