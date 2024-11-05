using System.ComponentModel.DataAnnotations;

namespace PortalDoAluno.Application.DTOs
{
    public class CreateAlunoDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O campo Usuario é obrigatório.")]
        [StringLength(50, ErrorMessage = "O Usuario deve ter no máximo 50 caracteres.")]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A Senha deve ter entre 6 e 50 caracteres.")]
        public required string Senha { get; set; }
    }
}
