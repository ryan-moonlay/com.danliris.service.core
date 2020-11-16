using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentAdditionalCharges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentAdditionalChargesDataUtil
    {
        private readonly GarmentAdditionalChargesService Service;

        public GarmentAdditionalChargesDataUtil(GarmentAdditionalChargesService service)
        {
            Service = service;
        }

        public GarmentAdditionalChargesModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentAdditionalChargesModel model = new GarmentAdditionalChargesModel
            {
                Name = $"Name{guid}",
            };

            return model;
        }

        public async Task<GarmentAdditionalChargesModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
