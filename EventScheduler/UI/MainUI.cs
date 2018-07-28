using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.Interfaces;
using EventScheduler.UI;

namespace EventScheduler.Interfaces
{
    public class MainUI : IUserInterface
    {
        public string UserInput { get ; set ; }

        public void DisplayScreen()
        {
            throw new NotImplementedException();
        }

        public void GetInput()
        {
            throw new NotImplementedException();
        }
    }
}
