using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator.Identifier
{
    public abstract class Identifier
    {
        public abstract string Id { get; set; }
        
        /**     
         * Returns for now just an example id in string format. 
         * Later could be using a Format class etc.
         */ 
        public abstract string Format { get; }
    }
}
