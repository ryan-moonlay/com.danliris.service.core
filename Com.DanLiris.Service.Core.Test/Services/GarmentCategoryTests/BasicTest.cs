using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Services.GarmentCategoryTests
{
    [Collection("ServiceProviderFixture Collection")]
    public class BasicTest : BasicServiceTest<CoreDbContext, GarmentCategoryService, GarmentCategory>
    {
        private static readonly string[] createAttrAssertions = { "Name","Code","UomId","CodeRequirement", "CategoryType" };
        private static readonly string[] updateAttrAssertions = { "Name", "Code", "UomId", "CodeRequirement", "CategoryType" };
        private static readonly string[] existAttrCriteria = { "Name" };

        public BasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private GarmentCategoryDataUtil DataUtil
        {
            get { return (GarmentCategoryDataUtil)ServiceProvider.GetService(typeof(GarmentCategoryDataUtil)); }
        }

        private GarmentCategoryService Services
        {
            get { return (GarmentCategoryService)ServiceProvider.GetService(typeof(GarmentCategoryService)); }
        }

        public override void EmptyCreateModel(GarmentCategory model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.UomId = null;
            model.CodeRequirement = string.Empty;
            model.CategoryType = string.Empty;
        }

        public override void EmptyUpdateModel(GarmentCategory model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.UomId = null;
            model.CodeRequirement = string.Empty;
            model.CategoryType = string.Empty;
        }

        public override GarmentCategory GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new GarmentCategory()
            {
                Name = String.Concat("TEST G-Category ", guid),
                Code = guid,
                CodeRequirement= String.Concat("TEST G-Category ", guid),
                CategoryType = String.Concat("TEST G-Category ", guid),
                UomUnit = String.Concat("TEST G-Category ", guid),
                UomId=1
            };
        }

        [Fact]
        public  void Should_Success_MapToViewModel()
        {
            GarmentCategory model =  DataUtil.GetNewData();
            var Response = Services.MapToViewModel(model);
            Assert.NotNull(Response);
        }

        [Fact]
        public void Should_Success_MapToModel()
        {
            GarmentCategoryViewModel model = DataUtil.GetNewData_GarmentCategoryViewModel();
            var Response = Services.MapToModel(model);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            GarmentCategory model = await DataUtil.GetTestDataAsync();

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
