using BusinessIdValidator.Identifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public class BusinessIdentifierSpecification : ISpecification<BusinessId> 
    {
        public const int BusinessIdLength = 9;
        public const int FirstPartLength = 7;
        public const int Divider = 11;             
        
        /*
         * Multipliers for each first seven first digits of BusinessId
         */
        protected int[] multipliers = new int[] { 7, 9, 10, 5, 8, 4, 2 };

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

        public bool IsSatisfiedBy(BusinessId businessId)
        {
            if (businessId == null)
            {
                reasonsForDissatisfaction.Add("Business Id cannot be null.");
                return false;
            }
            else if (businessId.Id == null)
            {
                reasonsForDissatisfaction.Add("Business Id's value cannot be null.");
                return false;
            }
            else if (String.IsNullOrEmpty(businessId.Id))
            {
                reasonsForDissatisfaction.Add("Business Id's value cannot be an empty string.");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(businessId.Id))
            {
                reasonsForDissatisfaction.Add("Business Id's value cannot be just white space(s).");
                return false;
            }

            if (businessId.Id.Length > BusinessIdLength)
            {
                reasonsForDissatisfaction.Add("Business Id is too long. It should be " + BusinessIdLength + " characters long.");
            }
            else if (businessId.Id.Length < BusinessIdLength)
            {
                reasonsForDissatisfaction.Add("Business Id is too short. It should be " + BusinessIdLength + " characters long.");

            }

            if (!businessId.Id.Contains('-'))
            {
                reasonsForDissatisfaction.Add("Business Id should contain '-' character.");

            }
            else
            {
                if (businessId.Id.Length > 1 &&
                    !CheckHelper.HasIntegerValue(businessId.Id.Substring(businessId.Id.LastIndexOf('-') + 1)))
                {
                    reasonsForDissatisfaction.Add("Business Id's last character should be numeric.");
                }

                if (businessId.Id.LastIndexOf('-') != FirstPartLength)
                {
                    reasonsForDissatisfaction.Add("Business Id should have '-' character as 8th character.");
                }
            }

            if (businessId.Id.Length >= 3 &&
                !CheckHelper.HasIntegerValue(businessId.Id.Substring(0, Math.Min(businessId.Id.Length - 3, FirstPartLength))))
            {
                reasonsForDissatisfaction.Add("Business Id's first seven characters should be numeric.");
            }

            InspectCorrectCheckDigit(businessId);

            return reasonsForDissatisfaction.Count == 0;
        }



        public bool InspectCorrectCheckDigit(BusinessId businessId)
        {
            if (businessId == null || businessId.Id == null 
                || businessId.Id.Length != BusinessIdLength)
            {
                return false;
            }
            else
            {
                try
                {
                    int sum = 0;
                    for (int i = 0; i < FirstPartLength; i++)
                    {
                        sum += Int32.Parse(businessId.Id[i].ToString()) * Multipliers[i];
                    }

                    int countedCheckDigit = sum % Divider;
                    if (countedCheckDigit == 1)
                    {
                        reasonsForDissatisfaction.Add("Business Id's first seven number's modulo 11 is not allowed to be 1.");
                        return false;
                    }
                    else if (countedCheckDigit > 1) 
                    {
                        countedCheckDigit = Divider - countedCheckDigit;
                    }

                    int givenCheckDigit = Int32.Parse(businessId.Id[BusinessIdLength - 1].ToString());

                    if (countedCheckDigit == givenCheckDigit)
                    {
                        return true;
                    }
                }
                catch (FormatException)
                {
                    reasonsForDissatisfaction.Add("Business Id should contain only numbers and one '-' character.");
                }
            }
            reasonsForDissatisfaction.Add("Business Id's check digit is not correct.");
            return false;
        }
    }


}
