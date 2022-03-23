using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    internal class Person
    {
        public string firstName; 
        public string lastName;
        public string Empl_ID;
        public string ssn;
        public string ot_hours;
        public string reg_hours; 

        public Person()
        {
            this.firstName = null;
            this.lastName = null;
            this.Empl_ID = null;
            this.ssn = null;
            this.ot_hours = null;
            this.reg_hours = null; 
        }

      
    }
}
