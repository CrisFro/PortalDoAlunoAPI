using System.ComponentModel.DataAnnotations;

namespace PortalDoAluno.Application.DTOs
{
    public class UpdateTurmaDto
    {
        [Required(ErrorMessage = "O ID da turma é obrigatório.")]
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O CursoId deve ser um valor positivo.")]
        public int CursoId { get; set; }

        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public required string Nome { get; set; }

        [Range(1900, 2100, ErrorMessage = "O Ano deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }
    }
}
