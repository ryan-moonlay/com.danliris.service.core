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

namespace Com.DanLiris.Service.Core.Test.Services.AccountBankTests
{
    [Collection("ServiceProviderFixture Collection")]
    public class Basic : BasicServiceTest<CoreDbContext, AccountBankService, AccountBank>
    {
        private static readonly string[] createAttrAssertions = { "BankName", "AccountName","AccountNumber","CurrencyId" };
        private static readonly string[] updateAttrAssertions = { "BankName", "AccountName", "AccountNumber", "CurrencyId" };
        private static readonly string[] existAttrCriteria = { "BankName",  "AccountNumber" };

        public Basic(ServiceProviderFixture fixture) : base(fixture, createAttrAssertions, updateAttrAssertions, existAttrCriteria)
        {
        }

        private AccountBankDataUtil DataUtil
        {
            get { return (AccountBankDataUtil)ServiceProvider.GetService(typeof(AccountBankDataUtil)); }
        }
        private AccountBankService Services
        {
            get { return (AccountBankService)ServiceProvider.GetService(typeof(AccountBankService)); }
        }

        public override void EmptyCreateModel(AccountBank model)
        {
            model.BankName = string.Empty;
            model.AccountName = string.Empty;
            model.AccountNumber = string.Empty;
            model.CurrencyId = null;
        }

        public override void EmptyUpdateModel(AccountBank model)
        {
            model.BankName = string.Empty;
            model.AccountName = string.Empty;
            model.AccountNumber = string.Empty;
            model.CurrencyId = null;
        }

        public override AccountBank GenerateTestModel()
        {
            string guid = Guid.NewGuid().ToString();

            return new AccountBank()
            {
                Code = guid,
                BankCode = guid,
                AccountName = "TEST BANK" + guid,
                BankName = "TEST BANK" + guid,
                AccountNumber = "TEST BANK" + guid,
                BankAddress = "TEST BANK",
                CurrencyCode = "TEST BANK",
                CurrencyDescription = "TEST BANK",
                DivisionCode = "TEST BANK",
                DivisionName = "TEST BANK",
                Fax = "TEST BANK",
                Phone = "TEST BANK",
                CurrencySymbol = "IDR",
                CurrencyRate=1,
                SwiftCode = "TEST BANK",
                DivisionId=1,
                CurrencyId=1,
                AccountCOA = "COA"
            };
        }

        [Fact]
        public async void Should_Success_ReadModel()
        {
            AccountBank model = await DataUtil.GetTestDataAsync();
            
            var orderData = new
            {
                Code = "asc"
            };
            string order = JsonConvert.SerializeObject(orderData);

            var Response = Services.ReadModel(1, 25, order, new List<string>(), "", "{}");
            Assert.NotNull(Response);
        }


        [Fact]
        public void Should_Success_MapToModel()
        {
            AccountBankViewModel viewModel =  DataUtil.GetEmptyData();

            var Response = Services.MapToModel(viewModel);
            Assert.NotNull(Response);
        }

    }
}