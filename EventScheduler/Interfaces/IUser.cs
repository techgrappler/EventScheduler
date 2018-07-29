using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.Interfaces
{
    public interface IUser
    {
        string FName { get; set; }
        string LName { get; set; }
        int ID { get; set; }
    }
}
