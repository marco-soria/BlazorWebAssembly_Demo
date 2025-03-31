namespace Blazor.Server.Models
{
    public class Department
    {
        public int IdDepartment { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
