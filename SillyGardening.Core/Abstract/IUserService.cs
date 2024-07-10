using SillyGardening.Core.Responce;
using SillyGardening.Web.Models;

namespace SillyGardening.Core.Abstract
{
    public interface IUserService
    {
        public void UserSave();
        public void UserLogOut();

        public AuthResponce UserLogin(UserViewModel user);
        public AuthResponce UserRegister(UserViewModel user);
    }
}