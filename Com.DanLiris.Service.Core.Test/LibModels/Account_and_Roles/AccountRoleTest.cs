using Com.DanLiris.Service.Core.Lib.Models.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.LibModels.Account_and_Roles
{
   public class AccountRoleTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            AccountRole category = new AccountRole()
            {
                Id = 1,
                AccountId = 1,
                UId = "UId",
                Role=new Role()
                {
                    Description= "Description",
                    Code= "Code",
                    Name= "Name",
                    AccountRoles=new List<AccountRole>()
                    {
                        new AccountRole()
                    },
                    Permissions=new List<Permission>()
                    {
                        new Permission()
                    }
                },
                RoleId=1,
                
            };
            Assert.Equal(1, category.Id);
            Assert.Equal(1, category.AccountId);
            Assert.NotNull(category.Role);
            Assert.Equal(1, category.RoleId);
        }
    }
}
