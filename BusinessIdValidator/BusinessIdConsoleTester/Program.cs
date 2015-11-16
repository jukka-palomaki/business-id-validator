using BusinessIdValidator;
using BusinessIdValidator.Identifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Give the business id: (for ending the program press Control-C or close the window)");
                string givenValue = Console.ReadLine();

                IdentifierSpecificationFactory<BusinessId> factory = new IdentifierSpecificationFactory<BusinessId>();
                ISpecification<BusinessId> spec = factory.GetSpecification();

                bool ok = spec.IsSatisfiedBy(new BusinessId(givenValue));
                if (ok)
                {
                    Console.WriteLine("Your business id was correct!");
                }
                else
                {
                    IEnumerable<string> reasons = spec.ReasonsForDissatisfaction;

                    foreach (var reason in reasons)
                    {
                        Console.WriteLine(reason);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
