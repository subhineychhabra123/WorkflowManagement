using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class CustomLookupVM
    {
        public int IDCustomLookup { get; set; }
        public int IDLookupType { get; set; }
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public int Sequence { get; set; }
    }
}