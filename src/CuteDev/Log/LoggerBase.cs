using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Logger
{
    public class LoggerBase
    {
        public string baseDriectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}
