using Blazor.Server.Data;
using Blazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DbCrudBlazorDbContext _dbContext;

        public DepartamentoController(DbCrudBlazorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        { 
            var responseApi = new ResponseAPI<List<DepartmentDTO>>();
            var listDepartment = new List<DepartmentDTO>();

            try
            {
                foreach (var item in await _dbContext.Departments.ToListAsync())
                { 
                    listDepartment.Add(new DepartmentDTO
                    {
                        IdDepartment = item.IdDepartment,
                        Name = item.Name
                    });
                    responseApi.IsCorrect = true;
                    responseApi.Value = listDepartment;
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;

            }
            return Ok(responseApi);
        }
    }
}
