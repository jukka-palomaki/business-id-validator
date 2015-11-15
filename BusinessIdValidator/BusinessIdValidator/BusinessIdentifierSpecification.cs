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
        public const int Divider = 11;             
        
        /*
         * Multipliers for each first seven first digits of BusinessId
         */
        private int[] multipliers = new int[] { 7, 9, 10, 5, 8, 4, 2 };

        public int[] Multipliers {
            get
            {
                return multipliers;
            }
       }

        private SortedSet<string> reasonsForDissatisfaction = new SortedSet<string>();

        public BusinessIdentifierSpecification()
        { 
        }

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
            else if (String.IsNullOrEmpty(businessId))
            {
                reasonsForDissatisfaction.Add("Business Id cannot be an empty string.");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(businessId))
            {
                reasonsForDissatisfaction.Add("Business Id cannot be just white space(s).");
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
            else
            {
                if (businessId.Length > 1 && 
                    !HasIntegerValue(businessId.Substring(businessId.LastIndexOf('-') + 1)))
                {
                    reasonsForDissatisfaction.Add("Business Id's last character should be numeric.");
                }

                if (businessId.LastIndexOf('-') != 7)
                {
                    reasonsForDissatisfaction.Add("Business Id should have '-' character as 8th character.");
                }
            }

            if (businessId.Length >= 3 &&
                !HasIntegerValue(businessId.Substring(0, Math.Min(businessId.Length-3, 7))))
            {
                reasonsForDissatisfaction.Add("Business Id's first seven characters should be numeric.");
            }

            if (!CheckCorrectCheckDigit(businessId)) 
            {
                //reasonsForDissatisfaction.Add("Business Id's check digit is not correct.");
            }

            return reasonsForDissatisfaction.Count == 0;
        }

        public static bool HasIntegerValue(string value)
        {
            try
            {
                Int32.Parse(value);
            }
            catch (FormatException e)
            {
                return false;
            }
            return true;
        }

        public bool CheckCorrectCheckDigit(string businessId)
        {
            if (businessId == null || businessId.Length != BusinessIdLength )
            {
                return false;
            }
            else
            {
                try
                {
                    int sum = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        sum += Int32.Parse(businessId[i].ToString()) * Multipliers[i];
                    }
                    
                    int countedCheckDigit = sum % 11;
                    if (countedCheckDigit == 1)
                    {
                        reasonsForDissatisfaction.Add("Business Id's first seven number's modulo 11 is not allowed to be 1.");
                        return false;
                    }
                    else if (countedCheckDigit > 1) 
                    {
                        countedCheckDigit = 11 - countedCheckDigit;
                    }

                    int givenCheckDigit = Int32.Parse(businessId[BusinessIdLength - 1].ToString());

                    if (countedCheckDigit == givenCheckDigit)
                    {
                        return true;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            reasonsForDissatisfaction.Add("Business Id's check digit is not correct.");
            return false;
        }
    }


}
