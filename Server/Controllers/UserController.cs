using SiweiSoft.SAPIService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class UserController : Controller
    {
        [Action("GET")]
        public ActionResult Get()
        {
            ActionResult ar = new ActionResult();
            ar.Result.Add("code", 200);

            UserSession se = (UserSession)Session;
            se.UserName = "FK";

            return ar;
        }
    }
}
