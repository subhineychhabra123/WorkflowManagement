using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Repository
{
  public  class UserXProjectXWorkFlowUserGroupRepository : BaseRepository<UserXProjectXWorkFlowUserGroup>
    {
      GIGEntities entities;
      public UserXProjectXWorkFlowUserGroupRepository(IUnitOfWork unit)
              : base(unit)
          {
              entities = (GIGEntities)this.UnitOfWork.Db;
          }
     
      public int? GetIsUserExistInThisLevel(Int32 IDProject, Int32 IDUser, Int32? sequence)
      {
          // ref int StatusCount = 0;
          ObjectParameter totalRecords = new ObjectParameter("totalRecords", typeof(int));
          int? count = entities.RFI_GetUSercurrentLevelsCount(IDProject, IDUser, sequence, totalRecords);
          if (totalRecords.Value.ToString() != "")
              return Convert.ToInt32(totalRecords.Value);
          else
              return 0;
      }
     
    }
}

