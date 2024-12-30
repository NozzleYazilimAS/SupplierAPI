using Microsoft.AspNetCore.Mvc;
using SupplierAPI.Services;
using SupplierAPI.Models;
using System;
using System.Threading.Tasks;

namespace SupplierAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSettings(Guid id)
        {
            var settings = await _settingsService.GetSettingsByIdAsync(id);
            if (settings == null)
            {
                return NotFound();
            }
            return Ok(settings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSettings([FromBody] Settings settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _settingsService.CreateSettingsAsync(settings);
            return CreatedAtAction(nameof(GetSettings), new { id = settings.Id }, settings);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSettings(Guid id, [FromBody] Settings settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSettings = await _settingsService.GetSettingsByIdAsync(id);
            if (existingSettings == null)
            {
                return NotFound();
            }

            await _settingsService.UpdateSettingsAsync(id, settings);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSettings(Guid id)
        {
            var existingSettings = await _settingsService.GetSettingsByIdAsync(id);
            if (existingSettings == null)
            {
                return NotFound();
            }

            await _settingsService.DeleteSettingsAsync(id);
            return NoContent();
        }
    }
}
