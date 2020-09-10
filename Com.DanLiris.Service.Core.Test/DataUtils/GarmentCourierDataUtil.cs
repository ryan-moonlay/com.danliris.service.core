using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentCourier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentCourierDataUtil
    {
        private readonly GarmentCourierService Service;

        public GarmentCourierDataUtil(GarmentCourierService service)
        {
            Service = service;
        }

        public GarmentCourierModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentCourierModel model = new GarmentCourierModel
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

        public async Task<GarmentCourierModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
