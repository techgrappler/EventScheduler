using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventScheduler.DBClasses;
using EventScheduler.Interfaces;

namespace EventScheduler
{
    public class Service : IDBItem
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

        public bool IsAny(int serviceID)
        {
            var services = UseDB.SelectService(serviceID);
            if (services.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
