namespace PortalDoAluno.Domain.Entities
{
    public class Turma
    {
        public int Id { get; set; }
        public Curso CursoId { get; set; }
        public required string Nome { get; set; }
        public int Ano { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
