using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler.Interfaces
{
    public interface IDBItem
    {
        bool IsAny(int idNum);
    }
}
