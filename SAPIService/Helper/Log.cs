using System;

namespace SiweiSoft.SAPIService.Helper
{
    /// <summary>
    /// Comment type
    /// </summary>
    public enum CommentType
    {
        Info,
        Warn,
        Error
    }

    public class Log
    {
        //Delegate for log
        public delegate void LogHandler(CommentType type, string comment);

        //Event for log -- In main thread
        public static event LogHandler LogEventMainThread;

        //Event for log -- In child thread
        public static event LogHandler LogEventChildThread;

        /// <summary>
        /// Log comment in child thread
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public static void LogCommentC(CommentType type, string message, params object[] arg)
        {
            if (LogEventChildThread != null)
            {
                if (arg.Length == 0)
                    LogEventChildThread(type, message);
                else
                    LogEventChildThread(type, String.Format(message, arg));
            }
        }

        /// <summary>
        /// Log comment in main thread
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public static void LogCommentM(CommentType type, string message, params object[] arg)
        {
            if (LogEventMainThread != null)
            {
                LogEventMainThread(type, String.Format(message, arg));
            }
        }
    }
}
