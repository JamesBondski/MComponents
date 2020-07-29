using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MComponents.MGrid
{
    public interface IMGridDataAdapter<T>
    {
        IQueryable<T> GetQueryable();

        Task<long> GetTotalDataCount();

        Task Add(Guid pId, T pNewValue);

        Task Remove(Guid pId, T pValue);

        Task Update(Guid pId, T pValue);
    }
}
