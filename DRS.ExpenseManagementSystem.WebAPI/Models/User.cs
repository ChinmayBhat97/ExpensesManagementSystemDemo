using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
