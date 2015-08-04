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
        [ActionInfo("GET")]
        public ActionResult Get()
        {
            ActionResult ar = new ActionResult();
            ar.Result.Add("code", 200);

            Session se = GetSession<Session>();
            Se

            return ar;
        }
    }
}
