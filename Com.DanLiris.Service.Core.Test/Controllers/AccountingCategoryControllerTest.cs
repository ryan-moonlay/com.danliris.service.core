using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.AccountingCategory;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Controllers
{
    public class AccountingCategoryControllerTest
    {
        private ServiceValidationException GetServiceValidationExeption()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(Model, serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private AccountingCategory Model
        {
            get { return new AccountingCategory(); }
        }

        private AccountingCategoryController GetController(IServiceProvider serviceProvider)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            var controller = (AccountingCategoryController)Activator.CreateInstance(typeof(AccountingCategoryController), serviceProvider);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public void Get_WithoutException_ReturnOK()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<AccountingCategory>(new List<AccountingCategory>(), 0, new Dictionary<string, string>(), new List<string>()));
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_ReadThrowException_ReturnInternalServerError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_WithoutException_ReturnCreated()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Verifiable();

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.CreateModel(It.IsAny<AccountingCategory>())).ReturnsAsync(1);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Post(It.IsAny<AccountingCategory>());

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Throws(GetServiceValidationExeption());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.CreateModel(It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Post(It.IsAny<AccountingCategory>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Post_ThrowException_ReturnInternalServerError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Throws(new Exception());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.CreateModel(It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Post(It.IsAny<AccountingCategory>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_NotNullModel_ReturnOK()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).ReturnsAsync(Model);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.GetById(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_NullModel_ReturnNotFound()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).ReturnsAsync((AccountingCategory)null);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.GetById(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_ThrowException_ReturnInternalServerError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).Throws(new Exception());
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.GetById(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_InvalidId_ReturnBadRequest()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Verifiable();

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.UpdateModel(It.IsAny<int>(), It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Put(1, new AccountingCategory() { Id = 2 });

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ValidId_ReturnNoContent()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Verifiable();

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.UpdateModel(It.IsAny<int>(), It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Put(It.IsAny<int>(), new AccountingCategory());

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ThrowServiceValidationExeption_ReturnBadRequest()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Throws(GetServiceValidationExeption());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.UpdateModel(It.IsAny<int>(), It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Put(It.IsAny<int>(), It.IsAny<AccountingCategory>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Put_ThrowException_ReturnInternalServerError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(v => v.Validate(It.IsAny<AccountingCategory>())).Throws(new Exception());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.UpdateModel(It.IsAny<int>(), It.IsAny<AccountingCategory>())).ReturnsAsync(1);

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IValidateService))).Returns(validateServiceMock.Object);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Put(It.IsAny<int>(), It.IsAny<AccountingCategory>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_WithoutException_ReturnNoContent()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).ReturnsAsync(Model);
            serviceMock.Setup(f => f.DeleteModel(It.IsAny<int>())).ReturnsAsync(1);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Delete(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_WithoutCorrectId_ReturnNotFound()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).ReturnsAsync((AccountingCategory)null);
            serviceMock.Setup(f => f.DeleteModel(It.IsAny<int>())).ReturnsAsync(1);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Delete(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Delete_ThrowException_ReturnInternalStatusError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IIdentityService))).Returns(new IdentityService());

            var serviceMock = new Mock<IAccountingCategoryService>();
            serviceMock.Setup(f => f.ReadModelById(It.IsAny<int>())).Throws(new Exception());
            serviceMock.Setup(f => f.DeleteModel(It.IsAny<int>())).ReturnsAsync(1);
            serviceProviderMock.Setup(sp => sp.GetService(typeof(IAccountingCategoryService))).Returns(serviceMock.Object);

            var controller = GetController(serviceProviderMock.Object);
            var response = await controller.Delete(It.IsAny<int>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
