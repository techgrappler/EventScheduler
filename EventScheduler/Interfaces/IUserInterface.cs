using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.Interfaces
{

    public interface IUserInterface
    {
        string UserInput { get; set; }
        void DisplayScreen();
        void DisplayScreen(int option);
        void GetInput();
    }
}
