using REQFINFO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class GIGVM
    {

        public string IDUPW { get; set; }
        public string IDContractor { get; set; }
        public System.Guid IDGig { get; set; }
        public string Number { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateRequired { get; set; }
        public Nullable<System.Guid> IDLifeCycle { get; set; }
        public string LoggedById { get; set; }
        public string OwnedByID { get; set; }
        public string AssignedToID { get; set; }

        public Nullable<System.DateTime> LoggedOn { get; set; }
        public string Detail { get; set; }
        public int IDFunction { get; set; }
        public string Name { get; set; }
        public string AssignedBy { get; set; }
        public string LoggedBy { get; set; }
        public string OwnedBy { get; set; }
        public string PastTenseFunction { get; set; }
        public string Originator { get; set; }
        public string GIGOriginatorName { get; set; }
        public string PreparedByName { get; set; }

        public string GIGOriginator { get; set; }
        public string PreparedBy { get; set; }
        public string ContractorGIGNumber { get; set; }
        public string SendTo { get; set; }
        public int? Sequence { get; set; }
        public UserVM UserVM { get; set; }

        public UserXProjectXWorkFlowUserGroupModel UserXProjectXWorkFlowUserGroupModel { get; set; }
        public ContractorVM ContractorVM { get; set; }
        public string ContractorName { get; set; }
        public string CurrentSequence { get; set; }
        public string previousSequence { get; set; }
        public string GIGDirection { get; set; }
        public string IDFunctionTrigger { get; set; }
    }
}