using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentForwarder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentForwarderDataUtil
    {
        private readonly GarmentForwarderService Service;

        public GarmentForwarderDataUtil(GarmentForwarderService service)
        {
            Service = service;
        }

        public GarmentForwarderModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentForwarderModel model = new GarmentForwarderModel
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

        public async Task<GarmentForwarderModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
