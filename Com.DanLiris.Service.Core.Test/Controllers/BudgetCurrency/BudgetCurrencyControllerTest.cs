using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Controllers.BudgetCurrency
{
   public class BudgetCurrencyControllerTest
    {
        protected BudgetCurrenciesController GetController(BudgetCurrencyService service)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            BudgetCurrenciesController controller = new BudgetCurrenciesController(service);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        [Fact]
        public void GetByIds_Return_OK()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            BudgetCurrencyService service =new  BudgetCurrencyService(serviceProvider.Object);
           
            //Act
            IActionResult response = GetController(service).GetByIds(new List<int>() { 1});

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }



        Mock<IServiceProvider> GetServiceProvider()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
              .Setup(s => s.GetService(typeof(IIdentityService)))
              .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var validateService = new Mock<IValidateService>();
            serviceProvider
              .Setup(s => s.GetService(typeof(IValidateService)))
              .Returns(validateService.Object);
            return serviceProvider;
        }
    }
}
