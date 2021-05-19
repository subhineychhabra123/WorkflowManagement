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
    public class AttachmentBusiness : AttachmentRepository, IAttachmentBusiness
    {
        private readonly AttachmentRepository attachmentRepository;
        public AttachmentBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            attachmentRepository = this;
        }
        public bool SaveAttachments(List<AttachmentModel> attachmentModel)
        {
            List<Attachment> attachment = new List<Attachment>();
            Mapper.Map(attachmentModel, attachment);
            attachmentRepository.InsertAll(attachment);
            return true;
        }
        public bool DeleteAttachments(List<AttachmentModel> DeleteAttachmentModel)
        {
            List<Attachment> attachment = new List<Attachment>();
            Mapper.Map(DeleteAttachmentModel, attachment);

            foreach (var AttachmentData in attachment)
            {
                attachmentRepository.Delete(x =>x.IDAttachment ==AttachmentData.IDAttachment);
            }
           
            return true;
        }

        
        public List<AttachmentModel> GetAttachments(Guid IDLifeCycle)
        {
            List<AttachmentModel> attachmentModel = new List<AttachmentModel>();
            List<Attachment> attachment = new List<Attachment>();           
            attachment = attachmentRepository.GetAll(x => x.IDLifeCycle == IDLifeCycle).ToList();
            Mapper.Map(attachment, attachmentModel);
            return attachmentModel;
        }



      
    }
}
