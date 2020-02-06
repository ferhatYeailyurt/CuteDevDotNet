using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CuteDev.Logger
{
    public class ApiLogger : LoggerBase, ILogger
    {
        private string serverUrl;
        private string ticket;

        private void SendLog(string message, int logLevel, object[] formatArgs)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                var url = serverUrl.TrimEnd('/') + "/data/logs/add";

                var prms = new
                {
                    Bilet = ticket,
                    appName = "CuteDev.Youtube.Haber",
                    logLevel = logLevel,
                    message = message,
                    logData = formatArgs.toJson()
                };

                try
                {
                    client.UploadStringAsync(new Uri(url), prms.toJson());
                    //var r = client.UploadString(new Uri(url), prms.toJson());
                }
                catch (WebException)
                {
                    //
                }
            }
        }


        public ApiLogger(string _serverUrl, string _ticket)
        {
            serverUrl = _serverUrl;
            ticket = _ticket;
        }

        public bool IsDebugEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Debug(string message, params object[] formatArgs)
        {
            try
            {
                SendLog(message, 3, formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }

        public void Error(string message, params object[] formatArgs)
        {
            try
            {
                SendLog(message, 2, formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }

        public void Error(Exception exception, string message, params object[] formatArgs)
        {
            try
            {
                if (message == null)
                    message = exception.Message;

                Error(message, exception.ToString(), formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }

        public void Error(Exception exception, params object[] formatArgs)
        {
            try
            {
                this.Error(exception, exception.Message, formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }

        public ILogger ForType(Type type)
        {
            throw new NotImplementedException();
        }

        public ILogger ForType<T>()
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] formatArgs)
        {
            try
            {
                SendLog(message, 0, formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }

        public void Warning(string message, params object[] formatArgs)
        {
            try
            {
                SendLog(message, 1, formatArgs);
            }
            catch (Exception)
            {
                // todo
            }
        }
    }
}
