using Blazor.Server.Data;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbCrudBlazorDbContext _dbContext;
        public EmpleadoController(DbCrudBlazorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmployeeDTO>>();
            var listEmployee = new List<EmployeeDTO>();

            try
            {
                foreach (var item in await _dbContext.Employees.Include(x => x.IdDepartmentNavigation).ToListAsync())
                {
                    listEmployee.Add(new EmployeeDTO
                    {
                        IdEmployee = item.IdEmployee,
                        FullName = item.FullName,
                        IdDepartment = item.IdDepartment,
                        Salary = item.Salary,
                        DateContract = item.DateContract,
                        Department = new DepartmentDTO
                        {
                            IdDepartment = item.IdDepartmentNavigation.IdDepartment,
                            Name = item.IdDepartmentNavigation.Name
                        }
                    });
                }
                responseApi.IsCorrect = true;
                responseApi.Value = listEmployee;
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmployeeDTO>();
            var employeeDto = new EmployeeDTO();

            try
            {
                var employeeFromDB = await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == id);
                if (employeeFromDB != null)
                {
                    employeeDto.IdEmployee = employeeFromDB.IdEmployee;
                    employeeDto.FullName = employeeFromDB.FullName;
                    employeeDto.IdDepartment = employeeFromDB.IdDepartment;
                    employeeDto.Salary = employeeFromDB.Salary;
                    employeeDto.DateContract = employeeFromDB.DateContract;

                    responseApi.IsCorrect = true;
                    responseApi.Value = employeeDto;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "Empleado no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmployeeDTO employee)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmployee = new Employee
                {
                    FullName = employee.FullName,
                    IdDepartment = employee.IdDepartment,
                    Salary = employee.Salary,
                    DateContract = employee.DateContract,
                };

                _dbContext.Employees.Add(dbEmployee);
                await _dbContext.SaveChangesAsync();

                var employeeFromDB =
                    await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == dbEmployee.IdEmployee);

                if (dbEmployee.IdEmployee != 0)
                {
                    responseApi.IsCorrect = true;
                    responseApi.Value = dbEmployee.IdEmployee;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "No se pudo guardar el empleado";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmployeeDTO employeeDto, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var employeeFromDB =
                    await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == id);

                if (employeeFromDB != null)
                {
                    employeeFromDB.FullName = employeeDto.FullName;
                    employeeFromDB.IdDepartment = employeeDto.IdDepartment;
                    employeeFromDB.Salary = employeeDto.Salary;
                    employeeFromDB.DateContract = employeeDto.DateContract;

                    await _dbContext.SaveChangesAsync();

                    responseApi.IsCorrect = true;
                    responseApi.Value = employeeFromDB.IdEmployee;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "Empleado no Actualizado";
                }

            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var employeeFromDB =
                    await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdEmployee == id);

                if (employeeFromDB != null)
                {
                    _dbContext.Employees.Remove(employeeFromDB);
                    await _dbContext.SaveChangesAsync();

                    responseApi.IsCorrect = true;
                    responseApi.Message = "Empleado eliminado con Exito";
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "Empleado no eliminado";
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