using Blazor.Client.Services.IService;
using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DepartmentDTO>> ListDepartments()
        {
            var result =
                await _httpClient.GetFromJsonAsync<ResponseAPI<List<DepartmentDTO>>>("api/Departmento/Lista");

            if (result!.IsCorrect)
            {
                return result.Value!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
    }
}
