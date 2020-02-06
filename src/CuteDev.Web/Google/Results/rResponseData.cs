using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Web.Google.Results
{
    public class rResponseData
    {

        public rCursor cursor { get; set; }

        public List<rImageResult> results { get; set; }
    }
}
