using ExpressMapper;
using REQFINFO.Business.Interfaces;
using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REQFINFO.Business
{
    public class LevelBusiness : LevelRepository, ILevelBusiness
    {
        private readonly LevelRepository LevelRepository;
        public LevelBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            LevelRepository = this;
        }

        public LevelModel GetDataForFields(Int32 IDWorkFlowUserGroup)
        {
            LevelModel levelModel = new LevelModel();
            Level Level = new Level();
            Level = LevelRepository.SingleOrDefault(x => x.IDWorkFlowUserGroup == IDWorkFlowUserGroup);

            Mapper.Map(Level, levelModel);
            return levelModel;
        }


        public LevelModel GetCurrentLevel(Int32 IDWorkFlowUserGroup, int? sequence)

        {
            LevelModel levelModel = new LevelModel();
            Level Level = new Level();
            Level = LevelRepository.SingleOrDefault(x => x.IDWorkFlowUserGroup == IDWorkFlowUserGroup && x.Sequence== sequence);

            Mapper.Map(Level, levelModel);
            return levelModel;
        }


        public LevelModel GetIDLevel(Int32 IDProject, Int32? LevelSquence,bool IsNextNotPreviousLevel)
        {
            LevelModel LevelModelObj = new LevelModel();
           

            GetLevelDetailNextOrPrevious_Result GetLevelDetailNextOrPrevious = LevelRepository.GetIDLevel(IDProject, LevelSquence, IsNextNotPreviousLevel);


            Mapper.Map(GetLevelDetailNextOrPrevious, LevelModelObj);



            return LevelModelObj;
        }
        //public LevelModel GetIDLevel(Int32? LevelSquence, Int32 IDProject, bool IsNextNotPreviousLevel)
        //{
        //    LevelModel LevelModelObj = new LevelModel();
        //    Level level = new Level();
        //    if (IsNextNotPreviousLevel)
        //    {
        //        level = LevelRepository.SingleOrDefault(x => x.Sequence > LevelSquence && x.WorkFlowUserGroup == IDProject);
        //    }
        //    else
        //    {

        //    }
            
            
        //    Mapper.Map(level, LevelModelObj);


            
        //    return LevelModelObj;
        //}
        
    }
}
