using System.ComponentModel.DataAnnotations;

namespace PortalDoAluno.Application.DTOs
{
    public class TurmaDto
    {
        public int? Id { get; set; }

        public int CursoId { get; set; }

        [Required(ErrorMessage = "O campo Turma é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 45 caracteres.")]
        public string Nome { get; set; } = string.Empty; 

        [Required(ErrorMessage = "O campo Ano é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "O Ano deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }

        public bool IsActive { get; set; } = true;

        public string CursoNome { get; set; } = string.Empty;
    }
}
