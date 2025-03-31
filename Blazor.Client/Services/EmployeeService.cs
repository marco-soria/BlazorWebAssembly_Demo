using Blazor.Client.Services.IService;
using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeDTO>> ListEmployees()
        {
            string fullRul = _httpClient.BaseAddress + "api/Empleado/Lista";

            var result =
                await _httpClient.GetFromJsonAsync<ResponseAPI<List<EmployeeDTO>>>("api/Empleado/Lista");

            if (result!.IsCorrect)
            {
                return result.Value!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        public async Task<EmployeeDTO> Buscar(int id)
        {
            var result =
                await _httpClient.GetFromJsonAsync<ResponseAPI<EmployeeDTO>>($"api/Empleado/Buscar/{id}");

            if (result!.IsCorrect)
            {
                return result.Value!;
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        public async Task<int> Guardar(EmployeeDTO employeeDTO)
        {
            var result =
                await _httpClient.PostAsJsonAsync("api/Empleado/Guardar", employeeDTO);

            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<int> Editar(EmployeeDTO employeeDTO)
        {
            var result =
                await _httpClient.PutAsJsonAsync($"api/Empleado/Editar/{employeeDTO.IdEmployee}", employeeDTO);

            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.Value!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var result =
                await _httpClient.DeleteAsync($"api/Empleado/Editar/{id}");

            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.IsCorrect)
            {
                return response.IsCorrect!;
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

    }
}
