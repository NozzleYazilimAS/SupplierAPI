using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupplierAPI.Models;

namespace SupplierAPI.Repositories
{
    public interface IMainRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : AuditableEntity;
        Task<List<T>> GetAllAsync<T>() where T : AuditableEntity;
        Task<T> AddAsync<T>(T entity) where T : AuditableEntity;
        Task<T> UpdateAsync<T>(T entity) where T : AuditableEntity;
        Task<bool> DeleteAsync<T>(Guid id) where T : AuditableEntity;
        Task<List<T>> GetByTenantIdAsync<T>(Guid tenantId) where T : AuditableEntity;
    }

    public class MainRepository : IMainRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly Guid _currentTenantId;
        private readonly bool _disableTenantFiltering;

        public MainRepository(ApplicationDbContext context, Guid currentTenantId, bool disableTenantFiltering = false)
        {
            _context = context;
            _currentTenantId = currentTenantId;
            _disableTenantFiltering = disableTenantFiltering;
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : AuditableEntity
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && (!_disableTenantFiltering || e.TenantId == _currentTenantId) && !e.IsDeleted);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : AuditableEntity
        {
            return await _context.Set<T>().Where(e => (!_disableTenantFiltering || e.TenantId == _currentTenantId) && !e.IsDeleted).ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : AuditableEntity
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : AuditableEntity
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync<T>(Guid id) where T : AuditableEntity
        {
            var entity = await GetByIdAsync<T>(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeletionTime = DateTime.UtcNow;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<T>> GetByTenantIdAsync<T>(Guid tenantId) where T : AuditableEntity
        {
            return await _context.Set<T>().Where(e => e.TenantId == tenantId && !e.IsDeleted).ToListAsync();
        }
    }
}
