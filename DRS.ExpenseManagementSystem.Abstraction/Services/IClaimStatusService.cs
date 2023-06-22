using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IClaimStatusService : IBaseService<ClaimStatus>
    {
        public Task<List<ClaimStatus>> StatusByRole(int role);
        public Task<bool> DeleteStatusById(int Id);
    }
}
