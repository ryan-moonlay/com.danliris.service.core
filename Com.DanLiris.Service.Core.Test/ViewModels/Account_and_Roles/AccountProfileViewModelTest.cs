using Com.DanLiris.Service.Core.Lib.ViewModels.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.ViewModels.Account_and_Roles
{
  public  class AccountProfileViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate_ProductDesignViewModel()
        {
            AccountProfileViewModel viewModel = new AccountProfileViewModel()
            {
                Firstname= "Firstname",
                Gender= "Gender",
                Lastname = "Lastname"
            };
            Assert.Equal("Gender", viewModel.Gender);
            Assert.Equal("Firstname", viewModel.Firstname);
            Assert.Equal("Lastname", viewModel.Lastname);
        }
    }
}
