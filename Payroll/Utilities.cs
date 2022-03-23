using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll
{
    public static class Utilities
    {
        public static double ConvertToDouble(string Value)
        {
            double output; 
            if(Value == null)
            {
                return 0;
            }
            else
            {
               
                double.TryParse(Value,out output); 

                if (double.IsNaN(output) || double.IsInfinity(output)){
                    return 0; 
                }

                return output;
            }
        }
    }
}
