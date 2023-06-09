﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserRepository userRepository;
      
        public UserService(IUserRepository _repository) : base(_repository)
        {
            this.userRepository = _repository;
            
        }

        public AuthenticationViewModel Authenticate(string EmployeeCode, string Password)
        {
            var userTable = userRepository.GetByEmployeeCodeAndPassword(EmployeeCode, Password);
            if (userTable == null)
            {
                return new AuthenticationViewModel
                {
                    IsAuthenticated = false,
                    ErrorMessage = "ERR001"
                };
            }
            else
            {
                return new AuthenticationViewModel
                {
                    
                    IsAuthenticated = true,
                    userDetails = userTable
                };
            }

            
        }

        public AuthenticationViewModel GetByEmpCodeAsync(string EmployeeCode)
        {
            var userInfo = userRepository.GetByEmployeeCodeAsync(EmployeeCode);
            return new AuthenticationViewModel
            {
                userDetails = userInfo
            };
        }

        public Task<List<User>> GetByRoleAsync(int role)
        {
            throw new NotImplementedException();
        }
    }
}
