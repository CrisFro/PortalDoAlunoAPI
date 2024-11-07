using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.RelacionarTurmas
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<AlunoDto> Alunos { get; set; } = [];
        public List<TurmaDto> Turmas { get; set; } = [];
        public List<TurmaComAlunosDto> TurmasComAlunos { get; set; } = [];

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task OnGetAsync()
        {
            var alunoResponse = await _httpClient.GetAsync("api/aluno");
            if (alunoResponse.IsSuccessStatusCode)
            {
                Alunos = await alunoResponse.Content.ReadFromJsonAsync<List<AlunoDto>>() ?? [];
            }

            var turmaResponse = await _httpClient.GetAsync("api/turma");
            if (turmaResponse.IsSuccessStatusCode)
            {
                Turmas = await turmaResponse.Content.ReadFromJsonAsync<List<TurmaDto>>() ?? [];
            }

            var turmasComAlunosResponse = await _httpClient.GetAsync("api/Relacionamentos/turmas-com-alunos");
            if (turmasComAlunosResponse.IsSuccessStatusCode)
            {
                TurmasComAlunos = await turmasComAlunosResponse.Content.ReadFromJsonAsync<List<TurmaComAlunosDto>>() ?? [];
            }
        }

        public async Task<IActionResult> OnPostAssociarAsync(int alunoId, int turmaId)
        {
            var relacionamentosResponse = await _httpClient.GetAsync($"api/Relacionamentos/relacionado?alunoId={alunoId}&turmaId={turmaId}");
            if (relacionamentosResponse.IsSuccessStatusCode)
            {
                var isAlreadyAssociated = await relacionamentosResponse.Content.ReadFromJsonAsync<bool>();
                if (isAlreadyAssociated)
                {
                    ModelState.AddModelError(string.Empty, "O aluno já está associado a esta turma.");
                    await CarregarDadosAsync();
                    return Page();
                }
            }

            var response = await _httpClient.PostAsync($"api/Relacionamentos/associar?alunoId={alunoId}&turmaId={turmaId}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["MensagemSucesso"] = "Aluno relacionado com sucesso!";
                return RedirectToPage(); 
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao relacionar o aluno à turma: {errorMessage}");
                await CarregarDadosAsync(); 
                return Page();
            }
        }

        private async Task CarregarDadosAsync()
        {
            var alunoResponse = await _httpClient.GetAsync("api/aluno");
            if (alunoResponse.IsSuccessStatusCode)
            {
                Alunos = await alunoResponse.Content.ReadFromJsonAsync<List<AlunoDto>>() ?? [];
            }

            var turmaResponse = await _httpClient.GetAsync("api/turma");
            if (turmaResponse.IsSuccessStatusCode)
            {
                Turmas = await turmaResponse.Content.ReadFromJsonAsync<List<TurmaDto>>() ?? [];
            }

            var turmasComAlunosResponse = await _httpClient.GetAsync("api/Relacionamentos/turmas-com-alunos");
            if (turmasComAlunosResponse.IsSuccessStatusCode)
            {
                TurmasComAlunos = await turmasComAlunosResponse.Content.ReadFromJsonAsync<List<TurmaComAlunosDto>>() ?? new List<TurmaComAlunosDto>();
            }
        }

        public async Task<IActionResult> OnPostInativarAsync(int alunoId, int turmaId)
        {
            var response = await _httpClient.PostAsync($"api/Relacionamentos/desativar?alunoId={alunoId}&turmaId={turmaId}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["MensagemSucesso"] = "Relação desativada com sucesso!";
                return RedirectToPage();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao desativar a relação: {errorMessage}");
                return Page();
            }
        }
    }
}
