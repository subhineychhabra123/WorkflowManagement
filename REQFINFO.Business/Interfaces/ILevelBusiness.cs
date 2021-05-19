using REQFINFO.Repository;
using REQFINFO.Domain;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REQFINFO.Repository.Infrastructure.Contract;

namespace REQFINFO.Business.Interfaces
{
    public interface ILevelBusiness
    {
        LevelModel GetDataForFields(Int32 IDWorkFlowUserGroup);
        LevelModel GetIDLevel(Int32 IDProject,Int32? LevelSquence, bool IsNextNotPreviousLevel);
        LevelModel GetCurrentLevel(Int32 IDWorkFlowUserGroup, int? sequence);
    }
}
