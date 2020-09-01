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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.DanLiris.Service.Core.Test.Services.CurrencyTest
{
    [Collection("ServiceProviderFixture Collection")]
    public class CurrencyBasicTest : BasicServiceTest<CoreDbContext, CurrencyService, Currency>
    {
        private static readonly string[] createAttrAssertions = { "Code", "Symbol", "Rate" };
        private static readonly string[] updateAttrAssertions = { "Code", "Symbol", "Rate" };
        private static readonly string[] existAttrCriteria = { "Code", "Description" };

        public CurrencyBasicTest(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private CurrencyDataUtil DataUtil
        {
            get { return (CurrencyDataUtil)ServiceProvider.GetService(typeof(CurrencyDataUtil)); }
        }

        private CurrencyService Services
        {
            get { return (CurrencyService)ServiceProvider.GetService(typeof(CurrencyService)); }
        }

        public override void EmptyCreateModel(Currency model)
        {
            model.Code = string.Empty;
            model.Symbol = string.Empty;
            model.Rate = -1;
        }

        public override void EmptyUpdateModel(Currency model)
        {
            model.Code = string.Empty;
            model.Symbol = string.Empty;
            model.Rate = -1;
        }

        public override Currency GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new Currency
            {
                Code = string.Format("CurrencyCode {0}", guid),
                Symbol = "IDR",
                Rate = 10,
                Description = string.Format("CurrencySymbol {0}", guid),
            };
        }

        [Fact]
        public async Task Should_Success_ReadModel()
        {
            Currency model = await DataUtil.GetTestDataAsync();

            var orderData = new
            {
                Code = "asc"
            };
            string order = JsonConvert.SerializeObject(orderData);

            var Response = Services.ReadModel(1, 25, order, new List<string>(), "", "{}");
            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Should_Success_UploadValidate()
        {
            Currency model = await DataUtil.GetTestDataAsync();
            List<CurrencyViewModel> currencyViewModel = new List<CurrencyViewModel>
            {
                new CurrencyViewModel
                {
                    Code="",
                    Description="",
                    Symbol="",
                    Rate=1,
                }
            };

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            KeyValuePair<string, StringValues> keyValue = new KeyValuePair<string, StringValues>("date", "2020-01-10");
            body.Add(keyValue);

            var Response = Services.UploadValidate(currencyViewModel, body);
            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Should_Success_UploadValidate_when_Rate_LessThanZero()
        {
            Currency model = await DataUtil.GetTestDataAsync();
            List<CurrencyViewModel> currencyViewModel = new List<CurrencyViewModel>
            {
                new CurrencyViewModel
                {
                    Code=model.Code,
                    Description=model.Description,
                    Symbol=model.Symbol,
                    Rate=-1,
                }
            };

            List<KeyValuePair<string, StringValues>> body = new List<KeyValuePair<string, StringValues>>();
            KeyValuePair<string, StringValues> keyValue = new KeyValuePair<string, StringValues>("date", "2020-01-10");
            body.Add(keyValue);

            var Response = Services.UploadValidate(currencyViewModel, body);
            Assert.NotNull(Response);
        }
    }
}
