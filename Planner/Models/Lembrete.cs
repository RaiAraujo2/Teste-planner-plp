using Planner.Models.Enum;

namespace Planner.Models
{
    public class Lembrete
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoLembrete TipoLembrete { get; set; }
        
        //TODO: Implementar ou não frequência de lembrete
    }
}
