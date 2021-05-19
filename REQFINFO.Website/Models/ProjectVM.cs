using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class ProjectVM
    {
        public string IDProject { get; set; }
        public int IDCompany { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IDUPW { get; set; }
        
    }
}