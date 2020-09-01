using Com.Danliris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.DataUtils;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
namespace Com.DanLiris.Service.Core.Test.Services.BuyerTest
{
    [Collection("ServiceProviderFixture Collection")]
    public class BuyerBasicTest : BasicServiceTest<CoreDbContext, BuyerService, Buyer>
    {
        private static readonly string[] createAttrAssertions = { "Name", "Code", "Country", "Type", "Tempo", "NIK" };
        private static readonly string[] updateAttrAssertions = { "Name", "Code", "Country", "Type", "Tempo", "NIK" };
        private static readonly string[] existAttrCriteria = { "Code" };
        public BuyerBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private BuyerDataUtil DataUtil
        {
            get { return (BuyerDataUtil)ServiceProvider.GetService(typeof(BuyerDataUtil)); }
        }

        private BuyerService Services
        {
            get { return (BuyerService)ServiceProvider.GetService(typeof(BuyerService)); }
        }
        public override void EmptyCreateModel(Buyer model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.Country = string.Empty;
            model.Type = string.Empty;
            model.NPWP = string.Empty;
            model.Tempo = -1;
            model.NIK = string.Empty;
        }

        public override void EmptyUpdateModel(Buyer model)
        {
            model.Code = string.Empty;
            model.Name = string.Empty;
            model.Country = string.Empty;
            model.Type = string.Empty;
            model.NPWP = string.Empty;
            model.Tempo = -1;
            model.NIK = string.Empty;
        }

        public override Buyer GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Buyer()
            {
                Code = string.Format("BuyerCode {0}", guid),
                Name = string.Format("BuyerName {0}", guid),
                Country = string.Format("BuyerCountry {0}", guid),
                Type = string.Format("BuyerType {0}", guid),
                NPWP = string.Format("BuyerNPWP {0}", guid),
                NIK = string.Format("BuyerNIK {0}", guid)
            };
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            Buyer model = await DataUtil.GetTestDataAsync();

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
            Buyer model = await DataUtil.GetTestDataAsync();
            List<BuyerViewModel> garmentCurrencies = new List<BuyerViewModel>
            {
                new BuyerViewModel
                {
                    Code="",
                    City=""
                }
            };

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            KeyValuePair<string, StringValues> keyValue = new KeyValuePair<string, StringValues>("date", "2020-01-10");
            body.Add(keyValue);

            var Response = Services.UploadValidate(garmentCurrencies, body);
            Assert.NotNull(Response);
        }
    }
}
