using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public class CheckHelper
    {

        public static bool HasIntegerValue(string value)
        {
            try
            {
                Int64.Parse(value);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

     }
}
