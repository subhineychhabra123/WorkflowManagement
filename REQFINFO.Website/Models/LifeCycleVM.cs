using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REQFINFO.Website.Models
{
    public class LifeCycleVM
    {
        public System.Guid IDLifeCycle { get; set; }
        public System.Guid IDGig { get; set; }
        public string IDLevelXFunction { get; set; }
        public string LoggedByID { get; set; }
        public string OwnedByID { get; set; }
        public string AssignedToID { get; set; }
        public System.DateTime LoggedOn { get; set; }
        public string Detail { get; set; }
        public bool ISSubmitted { get; set; }
        public List<AttachmentVM> AttachmentVM { get; set; }
        public string loggedbyFirstName { get; set; }
        public string loggedbyLastName { get; set; }
        public string PastTenseFunction { get; set; }
        public string SendtoFirstName { get; set; }
        public string SendtoLastName { get; set; }
        public int Attachment_Count { get; set; }
        public string ImageName { get; set; }
        public string IDLevel { get; set; }
        public  UserVM User { get; set; }
        public UserVM User1 { get; set; }
        public UserVM User2 { get; set; }
    }
}   