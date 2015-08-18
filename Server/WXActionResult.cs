using SiweiSoft.SAPIService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class WXActionResult : ActionResult
    {
        public override string GetResultString()
        {
            return base.Result.ContainsKey("single") ? base.Result["single"].ToString() : null;
        }
    }
}
