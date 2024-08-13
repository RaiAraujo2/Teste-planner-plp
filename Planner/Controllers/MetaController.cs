using Microsoft.AspNetCore.Mvc;
using Planner.Models.Enum;
using Planner.Models;
using Planner.Service;

namespace Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly MetaService _metaService;

        public MetaController(MetaService metaService)
        {
            _metaService = metaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeta(int id)
        {
            var meta = await _metaService.GetMetaByIdAsync(id);
            if (meta == null)
            {
                return NotFound();
            }
            return Ok(meta);
        }

        [HttpGet]
        public async Task<IActionResult> GetMetas([FromQuery] Categoria? categoria = null, [FromQuery] StatusMeta? status = null)
        {
            if (categoria.HasValue)
            {
                var metas = await _metaService.GetMetasByCategoriaAsync(categoria.Value);
                return Ok(metas);
            }

            if (status.HasValue)
            {
                var metas = await _metaService.GetMetasByStatusAsync(status.Value);
                return Ok(metas);
            }

            var allMetas = await _metaService.GetAllMetasAsync();
            return Ok(allMetas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeta([FromBody] Meta meta)
        {
            if (meta == null)
            {
                return BadRequest();
            }

            await _metaService.AddMetaAsync(meta);
            return CreatedAtAction(nameof(GetMeta), new { id = meta.Id }, meta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeta(int id, [FromBody] Meta meta)
        {
            if (meta == null || meta.Id != id)
            {
                return BadRequest();
            }

            var existingMeta = await _metaService.GetMetaByIdAsync(id);
            if (existingMeta == null)
            {
                return NotFound();
            }

            await _metaService.UpdateMetaAsync(meta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeta(int id)
        {
            var existingMeta = await _metaService.GetMetaByIdAsync(id);
            if (existingMeta == null)
            {
                return NotFound();
            }

            await _metaService.DeleteMetaAsync(id);
            return NoContent();
        }
    }
}
