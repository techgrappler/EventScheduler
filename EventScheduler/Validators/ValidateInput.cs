using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduler.Validators
{
    public class ValidateInput
    {
        public string InputType { get; set; }
        public virtual string ValidateString<T>(T arg)
        {
            this.InputType = arg.GetType().ToString();
            Console.WriteLine("The type is: {0}", this.InputType);
            return InputType;
        }

    }
}
