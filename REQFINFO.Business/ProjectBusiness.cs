using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Business.Interfaces;
using System.Collections.Generic;
using ExpressMapper;
using REQFINFO.Utility;
using System;

namespace REQFINFO.Business
{
   public class ProjectBusiness : ProjectRepository, IProjectBusiness
    {
      #region property
        private readonly ProjectRepository ProjectRepository;
        #endregion
        public ProjectBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            ProjectRepository = this;
        
        }
        public List<ProjectModel> GetProjectsList(Int32 IDUser, Int32 IDCompany, Int32 IDWorkFLow)
        {
           
            List<ProjectModel> ProjectModel = new List<ProjectModel>();
            List<RFI_GetProjectsList_Result> ProjectsList = ProjectRepository.GetUserProjectList(IDUser, IDCompany, IDWorkFLow);
            Mapper.Map(ProjectsList, ProjectModel);
            return ProjectModel;
        }
    }
}
