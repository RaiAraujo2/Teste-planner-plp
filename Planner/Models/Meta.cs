using Planner.Models.Enum;

namespace Planner.Models
{
    public class Meta : Atividade
    {
        public DateTime Prazo { get; set; }

        // Construtor padrão da classe Tarefa
        public Meta()
        {
        }

        // Construtor da classe Tarefa
        public Meta(int id, string titulo, string descricao, Categoria categoria, Status statusAtividade, DateTime prazo)
        : base(id, titulo, descricao, categoria, statusAtividade)
        {
            Prazo = prazo;
        }

        // Opcional: Construtor com parâmetro Descrição opcional
        public Meta(int id, string titulo, Categoria categoria, Status statusAtividade, DateTime prazo, string? descricao = null)
            : base(id, titulo, descricao, categoria, statusAtividade)
        {
            Prazo = prazo;
        }
    }

    
}
