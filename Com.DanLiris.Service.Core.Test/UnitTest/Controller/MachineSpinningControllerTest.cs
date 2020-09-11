using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Services.MachineSpinning;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.MachineSpinning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.UnitTest.Controller
{
  public  class MachineSpinningControllerTest
    {
        protected (Mock<IIdentityService> identityService, Mock<IValidateService> validateService,Mock<IMachineSpinningService> service,Mock<IMapper> mapper) GetMocks()
        {
            return (identityService: new Mock<IIdentityService>(), validateService: new Mock<IValidateService>(), service:new Mock<IMachineSpinningService>(),mapper :new Mock<IMapper>());
        }
        protected MachineSpinningController GetController((Mock<IIdentityService> identityService, Mock<IValidateService> validateService, Mock<IMachineSpinningService> service, Mock<IMapper> mapper) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);

            MachineSpinningController controller = new MachineSpinningController(mocks.identityService.Object,mocks.validateService.Object,mocks.service.Object,mocks.mapper.Object);
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

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public async Task PostCSVFileAsync_Return_Created()
        {
            //Setup
            var mocks = GetMocks();

            var validated = new Tuple<bool, List<object>>(true, new List<object>());
            mocks.service.Setup(s => s.UploadValidate(It.IsAny<List<MachineSpinningCsvViewModel>>(),It.IsAny<List<KeyValuePair<string, StringValues>>>())).Returns(validated);
            
            mocks.service.Setup(s => s.CsvHeader).Returns(new List<string>() { "This is a dummy file" });
            
            mocks.mapper.Setup(s => s.Map<List<MachineSpinningViewModel>>(It.IsAny<List<Lib.Models.MachineSpinningModel>>())).Returns(new List<MachineSpinningViewModel>());

            Mock<IFormFile> formFile = new Mock<IFormFile>();
            formFile.Setup(s => s.ContentType).Returns("multipart/form-data");
            formFile.Setup(s => s.Length).Returns(1);
            formFile.Setup(s => s.FileName).Returns("filename.csv");
            formFile.Setup(s => s.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")));

            MachineSpinningController controller =  GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            controller.ControllerContext.HttpContext.Request.Form= new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { formFile.Object });

            //Act
            var response =await controller.PostCSVFileAsync();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.Created, statusCode);
        }

        [Fact]
        public async Task PostCSVFileAsync_When_UploadValidate_NotValid()
        {
            //Setup
            var mocks = GetMocks();

            var validated = new Tuple<bool, List<object>>(false, new List<object>());
            mocks.service.Setup(s => s.UploadValidate(It.IsAny<List<MachineSpinningCsvViewModel>>(), It.IsAny<List<KeyValuePair<string, StringValues>>>())).Returns(validated);

            mocks.service.Setup(s => s.CsvHeader).Returns(new List<string>() { "This is a dummy file" });

            mocks.mapper.Setup(s => s.Map<List<MachineSpinningViewModel>>(It.IsAny<List<Lib.Models.MachineSpinningModel>>())).Returns(new List<MachineSpinningViewModel>());

            Mock<IFormFile> formFile = new Mock<IFormFile>();
            formFile.Setup(s => s.ContentType).Returns("multipart/form-data");
            formFile.Setup(s => s.Length).Returns(1);
            formFile.Setup(s => s.FileName).Returns("filename.csv");
            formFile.Setup(s => s.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")));

            MachineSpinningController controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers.Add("Content-Type", "multipart/form-data");
            controller.ControllerContext.HttpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { formFile.Object });

            //Act
            var response = await controller.PostCSVFileAsync();

            //Assert
            Assert.Equal("application/vnd.openxmlformats", response.GetType().GetProperty("ContentType").GetValue(response, null));
           
        }

        [Fact]
        public void GetSimple_Return_OK()
        {
            //Setup
            var mocks = GetMocks();
            mocks.service.Setup(s => s.GetSimple()).Returns(new List<Lib.Models.MachineSpinningModel>());
            mocks.mapper.Setup(s => s.Map<List<MachineSpinningViewModel>>(It.IsAny<List<Lib.Models.MachineSpinningModel>>())).Returns(new List<MachineSpinningViewModel>());

            //Act
            IActionResult response = GetController(mocks).GetSimple();

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetFilteredForSpinning_Return_OK()
        {
            //Setup
            var mocks = GetMocks();
            mocks.service.Setup(s => s.GetFilteredSpinning(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<Lib.Models.MachineSpinningModel>());
            mocks.mapper.Setup(s => s.Map<List<MachineSpinningViewModel>>(It.IsAny<List<Lib.Models.MachineSpinningModel>>())).Returns(new List<MachineSpinningViewModel>());

            //Act
            IActionResult response = GetController(mocks).GetFilteredForSpinning("Carding","1");

            //Assert
            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

    }
}
