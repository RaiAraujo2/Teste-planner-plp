namespace Planner.Models.Enum
{
    public enum Status
    {
        NaoIniciado,   // Representa que a atividade ainda não foi iniciada
        EmProgresso,   // Representa que a atividade está em andamento
        Concluido,     // Representa que a atividade foi concluída
        ParcialmenteConcluido, // Representa que a atividade foi parcialmente concluída
        Adiado         // Representa que a atividade foi adiada
    }
}
