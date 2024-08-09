using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementService.Domain.Primitives;

namespace SettlementService.Domain.Abstractions
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<Guid> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
    }
}
