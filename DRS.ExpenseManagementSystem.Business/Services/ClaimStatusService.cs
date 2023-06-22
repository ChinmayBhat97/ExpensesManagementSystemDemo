using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class ClaimStatusService : BaseService<ClaimStatus>, IClaimStatusService
    {
        private IClaimStatusRepository claimStatusRepository;
        public ClaimStatusService(IClaimStatusRepository repository) : base(repository)
        {
            this.claimStatusRepository = repository;
        }

        public async Task<bool> DeleteStatusById(int Id)
        {
            return await claimStatusRepository.DeleteStatusById(Id);
        }

        public async Task<List<ClaimStatus>> StatusByRole(int role)
        {
            return await claimStatusRepository.StatusByRole(role);
        }
    }
}
