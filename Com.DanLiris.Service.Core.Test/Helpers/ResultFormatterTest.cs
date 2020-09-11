using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.WebApi.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Helpers
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
            var result = formatter.Ok<Quality>(data, 1, 1, 10, 10, Order, Select);
            
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_OK_with_Mapping_and_Order()
        {
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            Func<MachineSpinningModel, MachineSpinningViewModel> resultMaping = new Func<MachineSpinningModel, MachineSpinningViewModel>(MapToViewModel);
            MachineSpinningModel data = new MachineSpinningModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);

            var mapperMock = new Mock<IMapper>();
            Dictionary<string, string> order = new Dictionary<string, string>();
            order.Add("Code", "asc");

            List<string> select = new List<string>();
          

            var result = formatter.Ok<MachineSpinningModel, MachineSpinningViewModel>(new List<MachineSpinningModel>() { data }, resultMaping,1, 25,1,1,order,select);
            Assert.True(0 < result.Count());
        }

        static MachineSpinningViewModel MapToViewModel(MachineSpinningModel basic)
        {
            return new MachineSpinningViewModel()
            {
                Code = basic.Code
            };
        }
    }
}
