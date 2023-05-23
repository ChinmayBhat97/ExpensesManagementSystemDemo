﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class IndividualExpenditureService : BaseService<IndividualExpenditure>, IIndividualExpenditureServices
    {
        private IIndividualExpenditureRepository repository;

        public IndividualExpenditureService(IIndividualExpenditureRepository repository) : base(repository)
        {
            this.repository = repository; 
        }

        //public async Task<List<IndividualExpenditure>> GetByBillingDate(DateTime billingDate)
        //{
        //    return await repository.GetByBillingDate(billingDate);
        //}

        //public async Task<List<IndividualExpenditure>> GetByCategory(string category)
        //{
        //    return await repository.GetByCategory(category);
        //}

        //public async Task<List<IndividualExpenditure>> GetByClaimID(int Id)
        //{
        //    return await repository.GetByClaimID(Id);
        //}

        //public Task<List<IndividualExpenditure>> GetByExpenseCategory(int Id)
        //{
        //    return repository.GetByExpenseCategory(Id);
        }
    }
}
