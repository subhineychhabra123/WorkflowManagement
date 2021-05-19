using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Repository
{
    public class LifeCycleRepository : BaseRepository<LifeCycle>
    {
        GIGEntities entities;
        public LifeCycleRepository(IUnitOfWork unit)
            : base(unit)
        {
            entities = (GIGEntities)this.UnitOfWork.Db;
        }


        public RFI_GetLevelXFunctionData_Result GetLevelXFunctionData(int IDUPW)
        {
            RFI_GetLevelXFunctionData_Result GetLevelXFunctionData = entities.RFI_GetLevelXFunctionData(IDUPW).SingleOrDefault();
            return GetLevelXFunctionData;

        }
        public List<RFI_GetGIGLogData_Result> GetallLifeCycleData(Guid IDGIG)
        {
            List<RFI_GetGIGLogData_Result> GetalllifeFunctionData = entities.RFI_GetGIGLogData(IDGIG).ToList();
            return GetalllifeFunctionData;

        }

        public RFI_GetCreateOption_Result GetLevelXFunctionID(Int32 IDWorkFlowUserGroup)
        {
            RFI_GetCreateOption_Result GetLevelXFunctionID = entities.RFI_GetCreateOption(IDWorkFlowUserGroup).SingleOrDefault();
            return GetLevelXFunctionID;

        }
    }
}