using Microsoft.EntityFrameworkCore;

namespace Planner.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Lembrete> lembretes { get; set; }


        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
    }
}
