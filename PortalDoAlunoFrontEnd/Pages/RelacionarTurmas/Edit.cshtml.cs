using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.RelacionarTurmas
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<AlunoDto> Alunos { get; set; } = [];
        public List<TurmaDto> Turmas { get; set; } = [];

        [BindProperty]
        public int AlunoId { get; set; }

        [BindProperty]
        public int TurmaId { get; set; }

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task OnGetAsync(int alunoId, int turmaId)
        {

            AlunoId = alunoId;
            TurmaId = turmaId;

            var alunoResponse = await _httpClient.GetAsync("api/aluno");
            if (alunoResponse.IsSuccessStatusCode)
            {
                Alunos = await alunoResponse.Content.ReadFromJsonAsync<List<AlunoDto>>();
            }

            var turmaResponse = await _httpClient.GetAsync("api/turma");
            if (turmaResponse.IsSuccessStatusCode)
            {
                Turmas = await turmaResponse.Content.ReadFromJsonAsync<List<TurmaDto>>();
            }
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/Relacionamentos/atualizar?alunoId={AlunoId}&turmaId={TurmaId}", null);
                if (response.IsSuccessStatusCode)
                {
                    TempData["MensagemSucesso"] = "Relação atualizada com sucesso!";
                    return RedirectToPage("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Erro ao atualizar a relação: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro inesperado: {ex.Message}");
            }

            await OnGetAsync(AlunoId, TurmaId);
            return Page();
        }
    }
}
