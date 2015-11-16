using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public interface ISpecification<in TEntity>
    {
        IEnumerable<string> ReasonsForDissatisfaction { get; }
        bool IsSatisfiedBy(TEntity entity);
    }

    /*
     * Add here all the id types for which you are going to implement a specification
     */ 
    public enum IdType
    {
        BusinessId,
        VatIdEu //not implemented

    }
}
