using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }

        [Required(ErrorMessage = "El campo FullName es obligatorio")]
        public string FullName { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "El campo IdDepartment es obligatorio")]
        public int IdDepartment { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "El campo Salary es obligatorio")]
        public int Salary { get; set; }

        public DateOnly DateContract { get; set; }

        public DepartmentDTO Department { get; set; }
    }
}
