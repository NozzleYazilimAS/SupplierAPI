using Microsoft.AspNetCore.Mvc;
using SupplierAPI.Services;
using SupplierAPI.Models;
using System;
using System.Threading.Tasks;

namespace SupplierAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenant(Guid id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTenant = await _tenantService.CreateTenantAsync(tenant);
            return CreatedAtAction(nameof(GetTenant), new { id = createdTenant.Id }, createdTenant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(Guid id, [FromBody] Tenant tenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTenant = await _tenantService.UpdateTenantAsync(id, tenant);
            if (updatedTenant == null)
            {
                return NotFound();
            }
            return Ok(updatedTenant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(Guid id)
        {
            var deleted = await _tenantService.DeleteTenantAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
