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
 public   class GarmentProductsControllerTest
    {
        protected GarmentProductsController GetController(GarmentProductService service)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            GarmentProductsController controller = new GarmentProductsController(service);
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

        public Lib.Models.GarmentProduct GetTestData(CoreDbContext dbContext)
        {
            Lib.Models.GarmentProduct data = new Lib.Models.GarmentProduct()
            {
                Code = "",
                Name=""
            };
            dbContext.GarmentProducts.Add(data);
            dbContext.SaveChanges();

            return data;
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


        [Fact]
        public void GetByIds_Return_OK()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetByIds(new List<int>() { testData.Id });

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public void GetByIds_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetByIds(new List<int>() { 1 });

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void GetByName_Return_Ok()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetByName("");


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetByName_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetByName(null);


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }


        [Fact]
        public void GetDistinctProductDesc_Return_Ok()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetDistinctProductDesc();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDistinctProductDesc_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetDistinctProductDesc();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void GetDistinctProductYarn_Return_Ok()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetDistinctProductYarn();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDistinctProductYarn_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetDistinctProductYarn();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void GetDistinctProductConst_Return_Ok()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetDistinctProductConst();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDistinctProductConst_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetDistinctProductConst();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public void GetDistinctProductWidth_Return_Ok()
        {
            //Setup
            CoreDbContext dbContext = GetDbContext(GetCurrentAsyncMethod());
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();

            GarmentProductService service = new GarmentProductService(serviceProvider.Object);

            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);
            serviceProvider.Setup(s => s.GetService(typeof(CoreDbContext))).Returns(dbContext);

            Lib.Models.GarmentProduct testData = GetTestData(dbContext);

            //Act
            IActionResult response = GetController(service).GetDistinctProductWidth();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetDistinctProductWidth_Return_InternalServerError()
        {
            //Setup
            Mock<IServiceProvider> serviceProvider = GetServiceProvider();
            GarmentProductService service = new GarmentProductService(serviceProvider.Object);
            serviceProvider.Setup(s => s.GetService(typeof(GarmentProductService))).Returns(service);

            //Act
            IActionResult response = GetController(service).GetDistinctProductWidth();


            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
