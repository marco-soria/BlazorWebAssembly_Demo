namespace Blazor.Server.Models
{
    public class Employee
    {
        public int IdEmployee { get; set; }
        public string FullName { get; set; }
        public int IdDepartment { get; set; }
        public int Salary { get; set; }
        public DateOnly DateContract { get; set; }
        public virtual Department IdDepartmentNavigation { get; set; } = null!;
    }
}
