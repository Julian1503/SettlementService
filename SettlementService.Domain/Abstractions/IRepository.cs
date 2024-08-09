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
        Guid Create(T entity);
        List<T> GetAll();
        T GetById(Guid id);
    }
}
