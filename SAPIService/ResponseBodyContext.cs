using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace SiweiSoft.SAPIService
{
    public class ResponseBodyContext<TSession> where TSession : Session
    {
        private Controller<TSession> controller;
        MethodInfo action = null;

        public static Dictionary<string, ControllerReflectionInfo> controllerInfoDictory;

        /// <summary>
        /// 初始化静态变量controllerDictory
        /// </summary>
        static ResponseBodyContext()
        {
            controllerInfoDictory = new Dictionary<string, ControllerReflectionInfo>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (type.Name.Length > 10 && type.Name.EndsWith("Controller"))
                {
                    string key = type.Name.Replace("Controller", null).ToUpper();
                    if (controllerInfoDictory.ContainsKey(key))
                        Log.LogCommentC(CommentType.Warn, "存在同名的Controller：{0}，可能引起冲突！", key);
                    else
                        controllerInfoDictory.Add(key, new ControllerReflectionInfo(type));
                }
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        /// <param name="context"></param>
        public ResponseBodyContext(Dictionary<string, object> config, HttpListenerContext context, TSession userSession)
        {
        }

        public ActionResult GetResponseBody()
        {
            ActionResult result = null;

            if (action != null)
                result = (ActionResult)action.Invoke(controller, null);

            return result;
        }
    }
}
