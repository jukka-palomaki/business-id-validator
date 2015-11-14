using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public class BusinessIdentifierSpecification : ISpecification<string> 
    {
        public const int BusinessIdLength = 9;

        private SortedSet<string> reasonsForDissatisfaction = new SortedSet<string>();

        public IEnumerable<string> ReasonsForDissatisfaction 
        {
            get 
            {
                return reasonsForDissatisfaction;  
            }
            
        }

        protected void AddReason(string reason)        
        {
            reasonsForDissatisfaction.Add(reason);
        }

        public bool IsSatisfiedBy(string businessId)
        {
            if (businessId == null)
            {
                reasonsForDissatisfaction.Add("Business Id cannot be null.");
                return false;
            }
            else if (businessId.Equals(""))
            {
                reasonsForDissatisfaction.Add("Business Id cannot be an empty string.");
                return false;
            }

            if (businessId.Length > BusinessIdLength)
            {
                reasonsForDissatisfaction.Add("Business Id is too long. It should be " + BusinessIdLength + " characters long.");
            }
            else if (businessId.Length < BusinessIdLength)
            {
                reasonsForDissatisfaction.Add("Business Id is too short. It should be " + BusinessIdLength + " characters long.");

            }

            if (!businessId.Contains('-'))
            {
                reasonsForDissatisfaction.Add("Business Id should contain '-' character.");
            }

            if (businessId.IndexOf('-') != 7)
            {
                reasonsForDissatisfaction.Add("Business Id should have '-' character as 8th character.");
            }

            //
            if (businessId[BusinessIdLength-1].Equals("1"))//todo 
            {
                reasonsForDissatisfaction.Add("Business Id's check digit should be numeric.");
            }

            return true;
        }
 
    }
}
