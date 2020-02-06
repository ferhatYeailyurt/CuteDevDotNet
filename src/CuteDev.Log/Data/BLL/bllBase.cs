/* Author: Volkan Sendag - vsendag@gmail.com */
using CuteDev;
using CuteDev.Entity.Parameters;
using CuteDev.Entity.Results;
using CuteDev.Log.Data.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Log.Data.BLL
{
    public class bllBase : CuteDev.Database.BLL.bllBase
    {
        internal static CuteModel getDb(string name = "CuteModelLog")
        {
            return new CuteModel(name);
        }
    }
}
