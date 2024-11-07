using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.Aluno
{
    public class FormModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public AlunoDto Aluno { get; set; } = new AlunoDto();

        public FormModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/aluno/{id.Value}");
                if (response.IsSuccessStatusCode)
                {
                    Aluno = await response.Content.ReadFromJsonAsync<AlunoDto>();
                }
                else
                {
                    return NotFound($"Aluno com ID {id} não encontrado.");
                }
            }
            else
            {
                Aluno = new AlunoDto();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpResponseMessage response;
            if (Aluno.Id.HasValue)
            {
                response = await _httpClient.PutAsJsonAsync($"api/aluno/{Aluno.Id}", Aluno);
            }
            else
            {
                response = await _httpClient.PostAsJsonAsync("api/aluno", Aluno);
            }

            if (response.IsSuccessStatusCode)
            {
                TempData["MensagemSucesso"] = Aluno.Id.HasValue ? "Aluno atualizado com sucesso." : "Aluno criado com sucesso.";
                return RedirectToPage("Index");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                
                ModelState.AddModelError("", string.IsNullOrWhiteSpace(errorMessage) ? "Erro ao salvar o aluno. Tente novamente." : errorMessage);
                return Page();
            }
        }
    }
}
