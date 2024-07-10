using SillyGardening.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyGardening.Core
{
    public class AuthResponce
    {
        public bool isSuccesful { get; set; }
        public UserViewModel? User { get; set; }
        public string? Comment { get; set; }

    }
}
