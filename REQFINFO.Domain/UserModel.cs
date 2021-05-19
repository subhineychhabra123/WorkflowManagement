using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
 public class UserModel
    {

        public int IDUser { get; set; }
        public int IDCompany { get; set; }
        public Nullable<int> IDContractor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string PhoneWork { get; set; }
        public string PhoneCell { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

      


    }
}
