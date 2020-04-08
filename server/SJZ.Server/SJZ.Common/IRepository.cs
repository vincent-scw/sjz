using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Common
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
    }
}
