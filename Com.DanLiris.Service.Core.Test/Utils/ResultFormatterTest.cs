using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.WebApi.Utils;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.DanLiris.Service.Core.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.DanLiris.Service.Core.Test.Utils
{
    public class ResultFormatterTest
    {
        [Fact]
        public void Should_Success_OK()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);

            var mapperMock = new Mock<IMapper>();
            var data = new List<Quality>() {
                new Quality(){
                    Id =1,
                    Name ="Name"
                }
            };

            Dictionary<string, string> Order = new Dictionary<string, string>();
            Order.Add("Name", "asc");

            List<string> Select = new List<string>()
            {
                "Name"
            };

            //Act
            var result = formatter.Ok<Quality>(mapperMock.Object,data, 1, 1, 10, 10, Order, Select);

            //Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void Fail_Return_Success()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            QualityViewModel viewModel = new QualityViewModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel);

            var errorData = new
            {
                WarningError = "Format Not Match"
            };

            string error = JsonConvert.SerializeObject(errorData);
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult(error, new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Fail_Throws_Exception()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            Quality model = new Quality();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult("errorMessaage", new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }
    }
}
