using Blazor.Shared;

namespace Blazor.Client.Services.IService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> ListEmployees();
        Task<EmployeeDTO> Buscar(int id);
        Task<int> Guardar(EmployeeDTO employeeDTO);
        Task<int> Editar(EmployeeDTO employeeDTO);

        Task<bool> Eliminar(int id);

    }
}
