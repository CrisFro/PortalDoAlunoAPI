using System.ComponentModel.DataAnnotations;

namespace PortalDoAluno.Application.DTOs
{
    public class UpdateAlunoDto
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        public required string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O Usuario deve ter no máximo 50 caracteres.")]
        public required string Usuario { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "A Senha deve ter entre 6 e 50 caracteres.")]
        public required string Senha { get; set; }
    }
}
