using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Service()
        {

        }
        public Service(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
