using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentEMKL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentEMKLDataUtil
    {
        private readonly GarmentEMKLService Service;

        public GarmentEMKLDataUtil(GarmentEMKLService service)
        {
            Service = service;
        }

        public GarmentEMKLModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentEMKLModel model = new GarmentEMKLModel
            {
                Code = $"Code{guid}",
                Name = $"Name{guid}",
                Address = $"Address{guid}",
                Attention = $"Attention{guid}",
                PhoneNumber = $"PhoneNumber{guid}",
                FaxNumber = $"FaxNumber{guid}",
                Email = $"Email{guid}",
                NPWP = $"NPWP{guid}",
            };

            return model;
        }

        public async Task<GarmentEMKLModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
