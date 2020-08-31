using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.UnitTest.Controller
{
    public class UnitsControllerTest
    {
        protected UnitsController GetController(UnitService service)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            UnitsController controller = new UnitsController(service);
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

        private CoreDbContext GetDbContext(string testName)
        {
            var serviceProvider = new ServiceCollection()
              .AddEntityFrameworkInMemoryDatabase()
              .BuildServiceProvider();

            DbContextOptionsBuilder<CoreDbContext> optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

            CoreDbContext dbContext = new CoreDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected string GetCurrentAsyncMethod([CallerMemberName] string methodName = "")
        {
            var method = new StackTrace()
                .GetFrames()
                .Select(frame => frame.GetMethod())
                .FirstOrDefault(item => item.Name == methodName);

            return method.Name;

        }

        public Lib.Models.Unit GetTestData(CoreDbContext dbContext)
        {
            Lib.Models.Unit data = new Lib.Models.Unit()
            {
                Code = ""
            };
            dbContext.Units.Add(data);
            dbContext.SaveChanges();

            return data;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        Mock<IServiceProvider> GetServiceProvider()
        {
            Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
              .Setup(s => s.GetService(typeof(IIdentityService)))
              .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var validateService = new Mock<IValidateService>();
            serviceProviderMock
              .Setup(s => s.GetService(typeof(IValidateService)))
              .Returns(validateService.Object);
            return serviceProviderMock;
        }

        [Fact]
        public void GetSimple_Return_OK()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

            UnitService service = new UnitService(serviceProviderMock.Object);

            serviceProviderMock.Setup(s => s.GetService(typeof(UnitService))).Returns(service);
            serviceProviderMock.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.Unit testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetSimple();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void GetSimple_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();
            UnitService service = new UnitService(serviceProviderMock.Object);
            serviceProviderMock.Setup(s => s.GetService(typeof(UnitService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetSimple();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }


        [Fact]
        public void GetWithVBDocumentLayoutOrder_Return_OK()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

            UnitService service = new UnitService(serviceProviderMock.Object);

            serviceProviderMock.Setup(s => s.GetService(typeof(UnitService))).Returns(service);
            serviceProviderMock.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.Unit testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetWithVBDocumentLayoutOrder();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void GetWithVBDocumentLayoutOrder_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

            UnitService service = new UnitService(serviceProviderMock.Object);

            serviceProviderMock.Setup(s => s.GetService(typeof(UnitService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetWithVBDocumentLayoutOrder();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

    }
}
