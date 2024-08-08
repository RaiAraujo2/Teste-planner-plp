using Microsoft.AspNetCore.Mvc;
using Planner.Models.Enum;
using Planner.Models;
using Planner.Service;

namespace Planner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarefa(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet]
        public async Task<IActionResult> GetTarefas([FromQuery] Categoria? categoria = null, [FromQuery] Status? status = null)
        {
            if (categoria.HasValue)
            {
                var tarefas = await _tarefaService.GetTarefasByCategoriaAsync(categoria.Value);
                return Ok(tarefas);
            }

            if (status.HasValue)
            {
                var tarefas = await _tarefaService.GetTarefasByStatusAsync(status.Value);
                return Ok(tarefas);
            }

            var allTarefas = await _tarefaService.GetAllTarefasAsync();
            return Ok(allTarefas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest();
            }

            await _tarefaService.AddTarefaAsync(tarefa);
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarefa(int id, [FromBody] Tarefa tarefa)
        {
            if (tarefa == null || tarefa.Id != id)
            {
                return BadRequest();
            }

            var existingTarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound();
            }

            await _tarefaService.UpdateTarefaAsync(tarefa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var existingTarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (existingTarefa == null)
            {
                return NotFound();
            }

            await _tarefaService.DeleteTarefaAsync(id);
            return NoContent();
        }
    }
}
