﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class UDFValidationDefinitionVM
    {
        public int IDUDFValidationDefinition { get; set; }
        public string ValidationType { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
    }
}