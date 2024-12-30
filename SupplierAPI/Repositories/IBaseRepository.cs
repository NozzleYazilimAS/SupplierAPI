using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupplierAPI.Models;

namespace SupplierAPI.Repositories
{
    public interface IBaseRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : AuditableEntity;
        Task<List<T>> GetAllAsync<T>() where T : AuditableEntity;
        Task<T> AddAsync<T>(T entity) where T : AuditableEntity;
        Task<T> UpdateAsync<T>(T entity) where T : AuditableEntity;
        Task<bool> DeleteAsync<T>(Guid id) where T : AuditableEntity;
        Task<List<T>> GetByTenantIdAsync<T>(Guid tenantId) where T : AuditableEntity;
    }
}
