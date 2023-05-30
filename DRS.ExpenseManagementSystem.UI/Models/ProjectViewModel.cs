namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Client { get; set; }
        public Employee Emp { get; set; }
    }
}
