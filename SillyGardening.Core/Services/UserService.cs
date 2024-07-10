using SillyGardening.Core.Abstract;
using SillyGardening.Core.Models;
using SillyGardening.DAL.Abstract;
using SillyGardening.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using SillyGardening.Core.GameComponents;
using System.ComponentModel;
using SillyGardening.Core.Responce;

namespace SillyGardening.Core.Services
{
    public class UserService : IUserService
    {
        private IRepository _repository;

        public UserViewModel? CurrentUser { get; set; }

        public UserService(IRepository repository)
        {
            _repository = repository;
        }


        public AuthResponce UserLogin(UserViewModel userVM)
        {
            var user = _repository.GetAll<User>().FirstOrDefault(u => u.UserName == userVM.UserName && u.Password == userVM.Password);
            if (user != null)
            {


                CurrentUser = MapUserVM(user);
                
                return new AuthResponce()
                {
                    isSuccesful = true,
                    Comment = "Logged in succesfully",
                    User = CurrentUser
                };
            }
            else
            {
                return new AuthResponce()
                {
                    isSuccesful = false,
                    Comment = "No user found! Try register",
                    User = userVM
                };
            }
        }

        public void UserSave()
        {
            if(CurrentUser != null)
            {
                _repository.Update(CurrentUser);
            }
        }

        public void UserLogOut()
        {
            CurrentUser = null;
        }

        public AuthResponce UserRegister(UserViewModel userVM)
        {
            if (!_repository.GetAll<User>().Any(u => u.UserName == userVM.UserName)) {

                //lotsa checks here mkay
                string comment = "An error has occured";
                if (userVM.UserName is null || userVM.UserName.Length < 6)
                {
                    comment = "Name is too short! Minimum length in 6 characters";
                }
                else if (userVM.Password is null || userVM.Password.Length < 6)
                {
                    comment = "Password is too short! Minimum length in 6 characters";
                }
                else if (userVM.UserName.Length > 128)
                {
                    comment = "Name is too long! Maximum length in 128 characters";
                }
                else if (userVM.Password.Length > 64)
                {
                    comment = "Password is too long! Maximum length in 64 characters";
                }

                else
                {
                    var newUser = MapUser(userVM);

                    _repository.Add(newUser);

                    CurrentUser = userVM;
                    return new AuthResponce()
                    {
                        isSuccesful = true,
                        Comment = "Registered succesfully",
                        User = CurrentUser
                    };
                }
                return new AuthResponce()
                {
                    isSuccesful = false,
                    Comment = comment,
                    User = userVM
                };
            }
            else
            {
                return new AuthResponce()
                {
                    isSuccesful = false,
                    Comment = "User with this name already exists!",
                    User = userVM
                };
            }
        }


        private User MapUser(UserViewModel userVM)
        {
            var user = new User();

            user.UserName = userVM.UserName;
            user.Password = userVM.Password;
            user.SaveData = JsonSerializer.Serialize(userVM.Game);

            return user;
        }

        private UserViewModel MapUserVM(User user)
        {
            var userVM = new UserViewModel();

            userVM.UserName = user.UserName;
            userVM.Password = user.Password;
            userVM.Game = JsonSerializer.Deserialize<GameSession>(user.SaveData);

            return userVM;
        }

    }
}
