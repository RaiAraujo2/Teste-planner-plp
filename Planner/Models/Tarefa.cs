using Planner.Models.Enum;

namespace Planner.Models
{
    public class Tarefa: Atividade
    {
        public TimeSpan Duracao { get; set; }

        // Construtor da classe Tarefa
        public Tarefa(int id, string titulo, string descricao, Categoria categoria, Status statusAtividade, TimeSpan duracao)
            : base(id, titulo, descricao, categoria, statusAtividade) // Chamada ao construtor da classe base
        {
            Duracao = duracao;
        }

        // Opcional: Construtor com parâmetro Descrição opcional
        public Tarefa(int id, string titulo, Categoria categoria, Status statusAtividade, TimeSpan duracao, string? descricao = null)
            : base(id, titulo, descricao, categoria, statusAtividade) // Chamada ao construtor da classe base
        {
            Duracao = duracao;
        }
    }
}
