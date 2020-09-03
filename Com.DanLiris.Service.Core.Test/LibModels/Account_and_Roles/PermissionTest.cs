using Com.DanLiris.Service.Core.Lib.Models.Account_and_Roles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.LibModels.Account_and_Roles
{
   public class PermissionTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            Permission model = new Permission()
            {
                permission = 1,
                Division = "Division",
                RoleId=1,
                Role=new Role(),
                Unit= "Unit",
                UnitCode= "UnitCode",
                UnitId=1,
                UId= "UId"
            };

            Assert.Equal(1, model.permission);
            Assert.Equal("Division", model.Division);
            Assert.Equal(1, model.RoleId);
            Assert.NotNull(model.Role);
            Assert.Equal("Division", model.Division);
        }

        [Fact]
        public void Should_Throws_NotImplementedException()
        {
            Permission model = new Permission();

            Assert.Throws<NotImplementedException>(() => model.Validate(null));
        }
    }
}
