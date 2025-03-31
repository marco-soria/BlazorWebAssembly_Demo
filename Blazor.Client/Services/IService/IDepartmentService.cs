using Blazor.Shared;

namespace Blazor.Client.Services.IService
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> ListDepartments();
    }
}
