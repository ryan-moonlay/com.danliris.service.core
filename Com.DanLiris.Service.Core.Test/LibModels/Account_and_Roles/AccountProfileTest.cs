using Com.DanLiris.Service.Core.Lib.Models.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.LibModels.Account_and_Roles
{
    public class AccountProfileTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            AccountProfile category = new AccountProfile()
            {
                Id = 1,
                AccountId = 1,
                Firstname = "Firstname",
                Gender = "Gender",
                Lastname = "Lastname",
                UId = "UId"
            };
            Assert.Equal(1, category.Id);
            Assert.Equal(1, category.AccountId);
            Assert.Equal("Firstname", category.Firstname);
            Assert.Equal("Lastname", category.Lastname);
            Assert.Equal("UId", category.UId);
            Assert.Equal("Gender", category.Gender);
        }
    }
}
