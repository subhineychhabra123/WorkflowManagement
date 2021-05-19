using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Domain;

namespace REQFINFO.Business.Interfaces
{
    public interface IStatusBusiness
    {
         List<StatusModel> GetAllStatus();
       
    }
}
