using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
  public  class ProjectModel
    {

        public int IDProject { get; set; }
        public int IDCompany { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IDUPW { get; set; }
    }
}
