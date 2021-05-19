using REQFINFO.Domain;
using REQFINFO.Repository;
using REQFINFO.Repository.DataEntity;
using REQFINFO.Repository.Infrastructure.Contract;
using REQFINFO.Business.Interfaces;
using System.Collections.Generic;
using ExpressMapper;
using REQFINFO.Utility;


namespace REQFINFO.Business
{
    public class UserBusiness : UserRepository, IUserBusiness
    {
        #region property
      private readonly UserRepository userRepository;


        #endregion
        public UserBusiness(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        {
            userRepository = this;
        }
        #region Login 
        public UserModel ValidateUser(string email, string password)
        {
            User user = userRepository.SingleOrDefault(x => x.Email == email && x.Password == password);
            UserModel userModel = new UserModel();
            Mapper.Map(user, userModel);
            
            return userModel;
        }

        #endregion


        #region ForgotPassword
        public UserModel ForgotPassword(string email)
        {
           
            User user = userRepository.SingleOrDefault(x => x.Email == email );
            UserModel userModel = new UserModel();
            if (user != null)
            {
                Mapper.Map(user, userModel);

            }

            return userModel;
        }



        public List<UserModel> GetSendto(int IDProject, int? sequence, int? IDContractor)
        {

            List<RFI_GetSendto_Result> Sendto = userRepository.GetSendto(IDProject, sequence, IDContractor);
            List<UserModel> userModel = new List<UserModel>();
            if (Sendto != null)
            {
                Mapper.Map(Sendto, userModel);

            }

            return userModel;
        }



        #endregion

    }
}
