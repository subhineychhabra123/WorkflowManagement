
using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business.Interfaces
{
    public interface IUserBusiness
    {
        UserModel ValidateUser(string email, string password);
        UserModel ForgotPassword(string email);
        List<UserModel> GetSendto(int IDProject, int? sequence, int? IDContractor);
        
    }
}
