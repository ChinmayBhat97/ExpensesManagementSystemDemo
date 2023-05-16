using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Username field is required")]
        [Display(Name = "Username")]
        public string? EmployeeCode { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
