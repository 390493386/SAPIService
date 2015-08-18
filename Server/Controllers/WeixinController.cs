using SiweiSoft.SAPIService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class WeixinController : Controller
    {
        [Action("WPTDEV", needAuthorize: false)]
        public WXActionResult Weipingtai()
        {
            var p = Parameters;
            var r = new WXActionResult();

            var ret = @"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";

            r.Result.Add("single", Parameters.ContainsKey("echostr") ? 
                Parameters["echostr"].ToString() : String.Format(ret,
                Parameters["FromUserName"].ToString(),
                Parameters["ToUserName"].ToString(),DateTime.Now.Ticks.ToString(), 
                "欢迎使用微信公共账号，您输入的内容为：" + Parameters["Content"].ToString() + "\r\n<a href=\"http://www.cnblogs.com\">点击进入</a>"));
            return r;
        }
    }
}
