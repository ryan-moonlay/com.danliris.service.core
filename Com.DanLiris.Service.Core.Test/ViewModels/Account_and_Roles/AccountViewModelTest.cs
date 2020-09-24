using Com.DanLiris.Service.Core.Lib.ViewModels.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.ViewModels.Account_and_Roles
{
    public class AccountViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            AccountViewModel viewModel = new AccountViewModel()
            {
                Id = 1,
                IsLocked=true,
                Password= "Password",
                Profile=new AccountProfileViewModel()
                {
                    Firstname= "Firstname",
                    Gender= "Gender",
                    Lastname= "Lastname"
                },
                UId= "UId",
                Username= "Username",
                Roles =new List<RoleViewModel>()
            };
            Assert.Equal("Password", viewModel.Password);
            Assert.Equal(1, viewModel.Id);
            Assert.True(viewModel.IsLocked);
            Assert.Equal("Username", viewModel.Username);
            Assert.Equal("UId", viewModel.UId);
            Assert.NotNull(viewModel.Profile);
            Assert.NotNull(viewModel.Roles);
        }
    }
}
