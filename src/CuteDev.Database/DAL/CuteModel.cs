using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CuteDev.Database.DAL
{
    public class CuteModel : DbContext
    {
        public CuteModel() : this("CuteModel")
        {
        }

        public CuteModel(string name) : base(name)
        {
            InitializeDatabase();
        }

        public virtual void InitializeDatabase()
        {

        }
    }
}
