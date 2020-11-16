using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.BICurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class BICurrencyDataUtil
    {
        private readonly BICurrencyService Service;

        public BICurrencyDataUtil(BICurrencyService service)
        {
            Service = service;
        }

        public BICurrency GetNewData()
        {
            Guid guid = Guid.NewGuid();

            BICurrency model = new BICurrency()
            {
                Code = "IDR",
                Name = "RUPIAH",
                Rate=1,
                Date=DateTime.Now
            };

            return model;
        }

        public async Task<BICurrency> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateModel(data);

            return data;
        }
    }
}
