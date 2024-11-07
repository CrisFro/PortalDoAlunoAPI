using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PortalDoAluno.Application.DTOs;

namespace PortalDoAlunoFrontend.Pages.Turma
{
    public class FormModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public TurmaDto Turma { get; set; }

        public FormModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var response = await _httpClient.GetAsync($"api/turma/{id.Value}");
                if (response.IsSuccessStatusCode)
                {
                    Turma = await response.Content.ReadFromJsonAsync<TurmaDto>();
                }
                else
                {
                    return NotFound($"Turma com ID {id} não encontrada.");
                }
            }
            else
            {
                Turma = new TurmaDto();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpResponseMessage response;
            if (Turma.Id.HasValue)
            {
                response = await _httpClient.PutAsJsonAsync($"api/turma/{Turma.Id}", Turma);
            }
            else
            {
                response = await _httpClient.PostAsJsonAsync("api/turma", Turma);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.AddModelError("Turma.Nome", "Já existe uma turma com este nome.");
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar a turma. Tente novamente.");
                return Page();
            }
        }
    }
}
