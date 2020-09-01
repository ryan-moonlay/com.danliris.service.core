using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Models = Com.DanLiris.Service.Core.Lib.Models;
namespace Com.DanLiris.Service.Core.Test.Services.Comodity
{
    [Collection("ServiceProviderFixture Collection")]
    public class ComodityBasicTest : BasicServiceTest<CoreDbContext, ComodityService, Models.Comodity>
    {
        private static readonly string[] createAttrAssertions = { "Name" };
        private static readonly string[] updateAttrAssertions = { "Name" };
        private static readonly string[] existAttrCriteria = { "Name" };
        public ComodityBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private ComodityServiceDataUtil DataUtil
        {
            get { return (ComodityServiceDataUtil)ServiceProvider.GetService(typeof(ComodityServiceDataUtil)); }
        }

        private ComodityService Services
        {
            get { return (ComodityService)ServiceProvider.GetService(typeof(ComodityService)); }
        }

        public override void EmptyCreateModel(Models.Comodity model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override void EmptyUpdateModel(Models.Comodity model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override Models.Comodity GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Models.Comodity()
            {
                Code = guid,
                Name = string.Format("TEST {0}", guid),
            };
        }

        [Fact]
        public void Should_Success_MapToViewModel()
        {
            var model = DataUtil.GetNewData();
            var Response = Services.MapToViewModel(model);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
           var model = await DataUtil.GetTestBuget();

            var orderData = new
            {
                Code = "asc"
            };
            string order = JsonConvert.SerializeObject(orderData);

            var Response = Services.ReadModel(1, 25, order, new List<string>(), "", "{}");
            Assert.NotNull(Response);
        }
    }
}
