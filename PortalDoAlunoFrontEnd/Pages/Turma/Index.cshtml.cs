using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.Turma
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<TurmaDto> Turmas { get; set; } = new List<TurmaDto>();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("api/turma");

            if (response.IsSuccessStatusCode)
            {
                Turmas = await response.Content.ReadFromJsonAsync<List<TurmaDto>>();
            }
            else
            {
                Turmas = new List<TurmaDto>();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/turma/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao desativar a turma.");
                return Page();
            }
        }
    }
}
