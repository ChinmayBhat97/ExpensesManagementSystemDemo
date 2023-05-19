namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class Auth
    {
        public bool isAuthenticated { get; set; }
        public User userDetails { get; set; }
        public string errorMessage { get; set; }
        public string authToken { get; set; }

        public Employee employeeDetails { get; set; }
    }
}
