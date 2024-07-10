using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using SillyGardening.Core.Abstract;
using SillyGardening.Core.GameComponents;
using SillyGardening.Core.Services;
using SillyGardening.Web.Models;
using System.Text.Json.Nodes;
using System.Timers;

namespace SillyGardening.Web.Controllers
{
    public class GameController : Controller
    {
        private IUserService _userService;
        private IGameSession _game;
        // Hook up the Elapsed event for the timer. 


        public GameController(IUserService userService, IGameSession game)
        {
            _userService = userService;
            _game = game;
        }


        public IActionResult Game()
        {
            return View("~/Views/Home/Game.cshtml", _game);
        }

        public JsonResult GameTick()
        {
            _game.Update();
            return Json(_game);
        }

        public JsonResult GetGameStats() =>
             Json(_game);
        public void RemovePlant(int id) =>
             _game.RemovePlant(id);


        public JsonResult Plant(int id) =>
             Json(_game.PlantPlant(id));

        public void HugPlant(int id) =>
            _game.HugPlant(id);



        public void Save() =>
            _userService.UserSave();
        public void LogOut()
        {
            _userService.UserLogOut();
            _game.ClearGameSession();
        }


        public JsonResult Register(UserViewModel user)
        {
            user.Game = (GameSession)_game;
            var responce = _userService.UserRegister(user);

            return Json(responce);
        }


        public JsonResult Login(UserViewModel user)
        {

            var responce = _userService.UserLogin(user);

            if(responce.isSuccesful) 
            { 
                _game.LoadGameSession(responce.User.Game);
            }

            return Json(responce);
        }
    }
}
