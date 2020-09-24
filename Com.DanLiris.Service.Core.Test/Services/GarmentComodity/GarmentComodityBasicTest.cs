using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using Models = Com.DanLiris.Service.Core.Lib.Models;

namespace Com.DanLiris.Service.Core.Test.Services.GarmentComodity
{
    [Collection("ServiceProviderFixture Collection")]
    public class GarmentComodityBasicTest : BasicServiceTest<CoreDbContext, GarmentComodityService, Models.GarmentComodity>
    {
        private static readonly string[] createAttrAssertions = { "Code", "Name" };
        private static readonly string[] updateAttrAssertions = { "Code", "Name" };
        private static readonly string[] existAttrCriteria = { "Code" };
        public GarmentComodityBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private GarmentComodityService Services
        {
            get { return (GarmentComodityService)ServiceProvider.GetService(typeof(GarmentComodityService)); }
        }

        private GarmentComodityDataUtil DataUtil
        {
            get { return (GarmentComodityDataUtil)ServiceProvider.GetService(typeof(GarmentComodityDataUtil)); }
        }

        public override void EmptyCreateModel(Models.GarmentComodity model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override void EmptyUpdateModel(Models.GarmentComodity model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override Models.GarmentComodity GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Models.GarmentComodity()
            {
                Code = guid,
                Name = string.Format("TEST {0}", guid),
            };
        }

        [Fact]
        public void Should_Success_MapToViewModel()
        {
            Models.GarmentComodity model = GenerateTestModel();

            var Response = Services.MapToViewModel(model);
            Assert.NotNull(Response);
            
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            var model = await DataUtil.GetTestDataAsync();

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
