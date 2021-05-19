using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Domain
{
  public  class LifeCycleModel
    {
        public System.Guid IDLifeCycle { get; set; }
        public System.Guid IDGig { get; set; }
        public Nullable<int> IDLevelXFunction { get; set; }
        public int LoggedByID { get; set; }
        public int? OwnedByID { get; set; }
        public Nullable<int> AssignedToID { get; set; }
        public System.DateTime LoggedOn { get; set; }
        public string Detail { get; set; }
        public bool ISSubmitted { get; set; }
        public  List<AttachmentModel> AttachmentModel{ get; set; }
        public string loggedbyFirstName { get; set; }
        public string loggedbyLastName { get; set; }
        public string PastTenseFunction { get; set; }
        public string SendtoFirstName { get; set; }
        public string SendtoLastName { get; set; }
        public int Attachment_Count { get; set; }
        public int IDLevel { get; set; }
        public string ImageName { get; set; }
        public LevelModel LevelModel { get; set; }
        public UserModel User { get; set; }
        public UserModel User1 { get; set; }
        public UserModel User2 { get; set; }
     
    }
}
