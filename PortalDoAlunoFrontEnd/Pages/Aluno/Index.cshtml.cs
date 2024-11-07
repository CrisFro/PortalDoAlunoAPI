using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.Aluno
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public List<AlunoDto> Alunos { get; set; } = [];

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API"); 
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("api/aluno");

            if (response.IsSuccessStatusCode)
            {
                Alunos = await response.Content.ReadFromJsonAsync<List<AlunoDto>>();
            }
            else
            {
                Alunos = []; 
            }
        }

        public async Task<IActionResult> OnPostInactivateAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/aluno/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["MensagemSucesso"] = "Aluno desativado com sucesso.";
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao desativar o aluno.");
                return Page();
            }
        }
    }
}
