namespace PortalDoAluno.Application.DTOs
{
    public class TurmaComAlunosDto
    {
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
        public List<AlunoDto> Alunos { get; set; } = [];

    }
}
