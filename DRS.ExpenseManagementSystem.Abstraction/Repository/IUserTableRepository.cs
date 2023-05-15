using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IUserTableRepository : IBaseRepository<UserTable>
    {
        //public Task<UserTable> GetByIsAccountLocked (bool isAccountLocked);
        //public Task<UserTable> GetByIsActived(bool isActive);
    }
}
