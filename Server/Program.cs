using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            SapiService service = new SapiService("192.168.1.86", 8889, serviceName: "sb");

            Log.LogEventChildThread += LogCommentC;
            Log.LogEventMainThread += LogCommentM;

            service.Initialize();
            service.Process<UserSession>();

            //service.Stop();
            Console.Read();
        }

        /// <summary>
        /// 主线程中调用的打印日志的方法
        /// </summary>
        /// <param name="type">信息类型</param>
        /// <param name="message">信息</param>
        public static void LogCommentM(CommentType type, string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// 子线程中调用的打印日志的方法
        /// </summary>
        /// <param name="type">信息类型</param>
        /// <param name="message">信息</param>
        public static void LogCommentC(CommentType type, string message)
        {
            Console.WriteLine(message);
        }
    }
}
