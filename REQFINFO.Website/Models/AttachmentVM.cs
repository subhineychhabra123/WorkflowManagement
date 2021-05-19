using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class AttachmentVM
    {
        public string IDAttachment { get; set; }
        public System.Guid IDLifeCycle { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool   IsDeleted { get; set; }
    }
}