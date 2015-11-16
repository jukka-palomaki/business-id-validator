using BusinessIdValidator.Identifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public class IdentifierSpecificationFactory<T>
    {
        public ISpecification<T> GetSpecification()
        {
            ISpecification<T> spec = null;

            Type type = typeof(T);

            if (type == typeof(BusinessId))
            {
                spec = (ISpecification<T>)new BusinessIdentifierSpecification(new int[] { 7, 9, 10, 5, 8, 4, 2 });
            }

            else if (type == typeof(VatId))
            {
                throw new NotImplementedException();
            }
            return spec;
        }

    }
}
