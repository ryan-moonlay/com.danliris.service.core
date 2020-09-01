using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Test.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class CurrencyDataUtil : BasicDataUtil<CoreDbContext, CurrencyService, Currency>, IEmptyData<CurrencyViewModel>
    {
        public CurrencyDataUtil(CoreDbContext dbContext, CurrencyService service) : base(dbContext, service)
        {
        }

        public CurrencyViewModel GetEmptyData()
        {
            return new CurrencyViewModel();
        }

        public override Currency GetNewData()
        {
            string guid = Guid.NewGuid().ToString();
            return new Currency
            {
                Code = "IDR",
                Symbol = "Rp",
                Rate = 1,
                UId="",
                Description = "",
                
            };
        }

        public override async Task<Currency> GetTestDataAsync()
        {
            var data = GetNewData();
            await Service.CreateModel(data);
            return data;
        }
    }
}
