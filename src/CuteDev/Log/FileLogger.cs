using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Logger
{
    public class FileLogger : LoggerBase, ILogger
    {
        private void LogYaz()
        {

        }

        public FileLogger()
        {

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
            throw new NotImplementedException();
        }

        public void Error(string message, params object[] formatArgs)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, string message, params object[] formatArgs)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Warning(string message, params object[] formatArgs)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception, params object[] formatArgs)
        {
            throw new NotImplementedException();
        }
    }
}
