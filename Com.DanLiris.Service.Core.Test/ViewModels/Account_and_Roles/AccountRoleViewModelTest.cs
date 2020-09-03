using Com.DanLiris.Service.Core.Lib.Models.Account_and_Roles;
using Com.DanLiris.Service.Core.Lib.ViewModels.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.ViewModels.Account_and_Roles
{
  public  class AccountRoleViewModelTest
    {

        [Fact]
        public void Should_Success_Instantiate()
        {
            AccountRoleViewModel viewModel = new AccountRoleViewModel()
            {
                AccountId=1,
                Id=1,
                Role=new Role()
                {
                    Code="Code"
                },
                RoleId=1
            };
            Assert.Equal(1, viewModel.AccountId);
            Assert.Equal(1, viewModel.Id);
            Assert.Equal(1, viewModel.RoleId);
            Assert.NotNull(viewModel.Role);
        }
    }
}
