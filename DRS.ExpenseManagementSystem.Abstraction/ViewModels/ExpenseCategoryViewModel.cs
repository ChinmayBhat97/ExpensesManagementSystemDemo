using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class ExpenseCategoryViewModel
    {
        public ExpenseCheckViewModel ExpenseCheckViewModel { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
