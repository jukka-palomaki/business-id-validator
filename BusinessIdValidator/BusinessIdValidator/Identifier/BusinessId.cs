using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator.Identifier
{
    public class BusinessId : Identifier
    {

        string id = "";

        public BusinessId(string id)
        {
            this.id = id;
        }

        public override string Id
        {
            get
            {
                return id;
            }
            set 
            {
                id = value;
            }
        }


        public override string Format 
        { 
            get
            {
                return "1234567-1";
            }
        }


    }
}
