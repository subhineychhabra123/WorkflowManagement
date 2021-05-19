using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
    public class GIGModel
    {

        public int IDUPW { get; set; }
        public Nullable<int> IDContractor { get; set; }
        public System.Guid IDGig { get; set; }
        public string Number { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateRequired { get; set; }
        public Nullable<System.Guid> IDLifeCycle { get; set; }
        public Nullable<int> LoggedById { get; set; }
        public Nullable<int> OwnedByID { get; set; }
        public Nullable<int> AssignedToID { get; set; }

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

        public int GIGOriginator { get; set; }
        public int PreparedBy { get; set; }
        public string ContractorGIGNumber { get; set; }
        public int SendTo { get; set; }
        public int? Sequence { get; set; }
        public UserModel UserModel { get; set; }
        public LifeCycleModel LifeCycleModel { get; set; }
        public UserXProjectXWorkFlowUserGroupModel UserXProjectXWorkFlowUserGroupModel { get; set; }
        public ContractorModel ContractorModel { get; set; }
        public string ContractorName { get; set; }
        public int IDProject { get; set; }
        public string GIGDirection { get; set; }
        public Nullable<int> CurrentSequence { get; set; }
        public Nullable<int> previousSequence { get; set; }
        public int IDFunctionTrigger { get; set; }
        
    }
}
