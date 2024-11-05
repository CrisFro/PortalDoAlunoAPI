namespace PortalDoAluno.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Usuario { get; set; }
        public required string Senha { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
