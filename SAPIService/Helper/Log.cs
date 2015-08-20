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

        //Event for log -- In child thread
        public static event LogHandler LogEvent;

        /// <summary>
        /// Log comment in child thread
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        public static void Comment(CommentType type, string message, params object[] arg)
        {
            if (LogEvent != null)
            {
                if (arg.Length == 0)
                    LogEvent(type, message);
                else
                    LogEvent(type, String.Format(message, arg));
            }
        }
    }
}
