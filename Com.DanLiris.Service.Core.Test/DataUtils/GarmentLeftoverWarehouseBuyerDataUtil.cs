using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseBuyer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentLeftoverWarehouseBuyerDataUtil
    {
        private readonly GarmentLeftoverWarehouseBuyerService Service;

        public GarmentLeftoverWarehouseBuyerDataUtil(GarmentLeftoverWarehouseBuyerService service)
        {
            Service = service;
        }

        public GarmentLeftoverWarehouseBuyerModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentLeftoverWarehouseBuyerModel model = new GarmentLeftoverWarehouseBuyerModel
            {
                Code = $"Code{guid}",
                Name = $"Name{guid}",
                Address = $"Address{guid}",
                PhoneNumber = $"PhoneNumber{guid}",
                NIK = $"NIK{guid}",
                NPWP = $"NPWP{guid}",
                WPName = $"WPName{guid}",
                KaberType = $"KaberType{guid}",
            };

            return model;
        }

        public async Task<GarmentLeftoverWarehouseBuyerModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
