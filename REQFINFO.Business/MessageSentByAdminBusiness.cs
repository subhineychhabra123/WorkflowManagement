using CLIPBOARD.Business.Interfaces;
using CLIPBOARD.Domain;
using CLIPBOARD.Repository;
using CLIPBOARD.Repository.DataEntity;
using CLIPBOARD.Repository.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIPBOARD.Business
{
 public   class MessageSentByAdminBusiness:MessageSentByAdminRepository,IMessageSentByAdminBusiness
    {
     MessageSentByAdminRepository messageSentByAdminRepository;
     public MessageSentByAdminBusiness(IUnitOfWork unit):base(unit)
     {
         messageSentByAdminRepository = new MessageSentByAdminRepository(unit);
     }
     public void SendMessageToUserOrBusiness(MessageModel messageSentByAdminModel)
     {
         Message messageSentByAdmin = new Message();
         AutoMapper.Mapper.Map(messageSentByAdminModel, messageSentByAdmin);
         MessageRecipient messageRecipient = new MessageRecipient();
         messageRecipient.CreatedDate = messageSentByAdminModel.CreatedDate;
         messageRecipient.MessageId = messageSentByAdmin.MessageId;
         messageRecipient.RecievedBy = messageSentByAdminModel.RecievedBy;
         messageSentByAdmin.MessageRecipient.Add(messageRecipient);
         messageSentByAdminRepository.Insert(messageSentByAdmin);

     }
    }
}
