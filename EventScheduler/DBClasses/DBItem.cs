using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;

namespace EventScheduler.DBClasses
{
    public abstract class DBItem : IDBItem
    {
        public bool IsAny(int idNum)
        {
            throw new NotImplementedException();
        }
    }
}
