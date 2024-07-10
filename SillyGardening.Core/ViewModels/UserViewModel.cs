using SillyGardening.Core.GameComponents;
using SillyGardening.Core.Services;

namespace SillyGardening.Web.Models
{
    public class UserViewModel
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public GameSession Game { get; set; }


    }
}
