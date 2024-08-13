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


        // Pega uma tarefa pelo id
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

        // Pega todas as tarefas ou filtra por categoria ou status
        // Exemplo de chamada: /api/tarefa?categoria=Estudo
        // Exemplo de chamada: /api/tarefa?status=Concluido
        // FromQuery é um atributo que indica que um parâmetro de ação deve ser vinculado a um valor de string de consulta da URL.
        [HttpGet]
        public async Task<IActionResult> GetTarefas([FromQuery] Categoria? categoria = null, [FromQuery] StatusTarefa? status = null)
        {
            if (categoria.HasValue && status.HasValue)
            {
                // Busca tarefas filtrando por categoria e status
                var tarefas = await _tarefaService.GetTarefasByCategoriaAndStatusAsync(status.Value, categoria.Value);
                return Ok(tarefas);
            }

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

        // Cria uma nova tarefa 
        [HttpPost]
        public async Task<IActionResult> CreateTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest();
            }
            if (tarefa.Dia == default(DateTime))  // Verifica se Dia não foi fornecido (é o valor padrão de DateTime)
            {
                tarefa.Dia = DateTime.Now.Date;  // Define como a data atual
            }

            await _tarefaService.AddTarefaAsync(tarefa);
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }


        //TODO: Nem esse nem os outros metodos PUT estão funcionado
        // Atualiza uma tarefa
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

        // Deleta uma tarefa
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

        // Deleta todas as tarefas
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTarefas()
        {
            await _tarefaService.DeleteAllTarefasAsync();
            return NoContent();
        }
    }
}
