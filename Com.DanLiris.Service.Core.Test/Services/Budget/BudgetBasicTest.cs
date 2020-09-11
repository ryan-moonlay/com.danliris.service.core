using System;
using Xunit;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Services;
using Models = Com.DanLiris.Service.Core.Lib.Models;
using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Newtonsoft.Json;
using System.Collections.Generic;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Microsoft.Extensions.Primitives;

namespace Com.DanLiris.Service.Core.Test.Service.Budget
{
    [Collection("ServiceProviderFixture Collection")]
    public class BudgetBasicTest : BasicServiceTest<CoreDbContext, BudgetService, Models.Budget>
    {
        private static readonly string[] createAttrAssertions = { "Code", "Name" };
        private static readonly string[] updateAttrAssertions = { "Code", "Name" };
        private static readonly string[] existAttrCriteria = { "Code" };

        public BudgetBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private BudgetService Services
        {
            get { return (BudgetService)ServiceProvider.GetService(typeof(BudgetService)); }
        }

        private BudgetServiceDataUtil DataUtil
        {
            get { return (BudgetServiceDataUtil)ServiceProvider.GetService(typeof(BudgetServiceDataUtil)); }
        }

        public override void EmptyCreateModel(Models.Budget model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override void EmptyUpdateModel(Models.Budget model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
        }

        public override Models.Budget GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Models.Budget()
            {
                Code = guid,
                Name = "TEST BUDGET",
            };
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

        [Fact]
        public async void Should_Success_UploadValidate()
        {
            var model = await DataUtil.GetTestDataAsync();
            List<BudgetViewModel> budgetViewModel = new List<BudgetViewModel>
            {
                new BudgetViewModel
                {
                    code = "",
                    name="",
                }
            };

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            KeyValuePair<string, StringValues> keyValue = new KeyValuePair<string, StringValues>("date", "2020-01-10");
            body.Add(keyValue);

            var Response = Services.UploadValidate(budgetViewModel, body);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void Should_Success_UploadValidate_when_DuplicateErrorMessage()
        {
            var model = await DataUtil.GetTestDataAsync();
            List<BudgetViewModel> budgetViewModel = new List<BudgetViewModel>
            {
                new BudgetViewModel
                {
                    code = model.Code,
                    name=model.Name,
                }
            };

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            KeyValuePair<string, StringValues> keyValue = new KeyValuePair<string, StringValues>("date", "2020-01-10");
            body.Add(keyValue);

            var Response = Services.UploadValidate(budgetViewModel, body);
            Assert.NotNull(Response);
        }
    }
}