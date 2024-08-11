using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementService.Domain.Primitives;

namespace SettlementService.Domain.Abstractions
{
    /// <summary>
    /// General repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository <T> where T : BaseEntity
    {
        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">Value to be saved</param>
        /// <returns>Guid</returns>
        Task<Guid> CreateAsync(T entity);

        /// <summary>
        /// Gets all entities from one type from the database
        /// </summary>
        /// <returns>A list of entities</returns>
        Task<List<T>> GetAllAsync();
        
        /// <summary>
        /// Gets an entity by its Id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>An entity</returns>
        Task<T> GetByIdAsync(Guid id);
    }
}
