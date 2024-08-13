using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.Service;

namespace Planner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LembreteController : ControllerBase
    {
        private readonly LembreteService _lembreteService;

        public LembreteController(LembreteService lembreteService)
        {
            _lembreteService = lembreteService;
        }

        // GET: api/lembrete
        [HttpGet]
        public async Task<IActionResult> GetLembretes()
        {
            var lembretes = await _lembreteService.GetLembretesAsync();
            return Ok(lembretes);
        }

        // GET: api/lembrete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLembreteById(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            return Ok(lembrete);
        }

        // GET: api/lembrete/hoje
        [HttpGet("hoje")]
        public async Task<IActionResult> GetLembretesParaHoje()
        {
            var lembretes = await _lembreteService.GetLembretesParaHojeAsync();
            return Ok(lembretes);
        }

        // GET: api/lembrete/amanha
        [HttpGet("amanha")]
        public async Task<IActionResult> GetLembretesParaAmanha()
        {
            var lembretes = await _lembreteService.GetLembretesParaAmanhaAsync();
            return Ok(lembretes);
        }

        // GET: api/lembrete/semana
        [HttpGet("semana")]
        public async Task<IActionResult> GetLembretesParaEstaSemana()
        {
            var lembretes = await _lembreteService.GetLembretesParaEstaSemanaAsync();
            return Ok(lembretes);
        }

        // GET: api/lembrete/mes
        [HttpGet("mes")]
        public async Task<IActionResult> GetLembretesParaEsteMes()
        {
            var lembretes = await _lembreteService.GetLembretesParaEsteMesAsync();
            return Ok(lembretes);
        }

        // POST: api/lembrete
        [HttpPost]
        public async Task<IActionResult> AdicionarLembrete([FromBody] Lembrete lembrete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lembreteService.AdicionarLembreteAsync(lembrete);
            return CreatedAtAction(nameof(GetLembreteById), new { id = lembrete.Id }, lembrete);
        }

        // PUT: api/lembrete/5
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarLembrete(int id, [FromBody] Lembrete lembreteAtualizado)
        {
            if (id != lembreteAtualizado.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var lembreteExistente = await _lembreteService.GetLembreteByIdAsync(id);
            if (lembreteExistente == null)
            {
                return NotFound();
            }

            await _lembreteService.AtualizarLembreteAsync(lembreteAtualizado);
            return NoContent();
        }

        // DELETE: api/lembrete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarLembrete(int id)
        {
            var lembrete = await _lembreteService.GetLembreteByIdAsync(id);

            if (lembrete == null)
            {
                return NotFound();
            }

            await _lembreteService.DeletarLembreteAsync(id);
            return NoContent();
        }

        // POST: api/lembrete/processar
        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarLembretes()
        {
            await _lembreteService.ProcessarLembretes();
            return Ok();
        }
    }
}
