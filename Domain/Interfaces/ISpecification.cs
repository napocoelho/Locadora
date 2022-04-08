using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Domain.Interfaces;

public interface ISpecification<T> where T: IEntity
{
    bool IsSatisfiedBy(T entity);
}
