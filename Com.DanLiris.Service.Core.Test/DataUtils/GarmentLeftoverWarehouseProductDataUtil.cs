using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentLeftoverWarehouseProductDataUtil
    {
        private readonly GarmentLeftoverWarehouseProductService Service;

        public GarmentLeftoverWarehouseProductDataUtil(GarmentLeftoverWarehouseProductService service)
        {
            Service = service;
        }

        public GarmentLeftoverWarehouseProductModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentLeftoverWarehouseProductModel model = new GarmentLeftoverWarehouseProductModel
            {
                Code = $"Code{guid}",
                Name = $"Name{guid}",
                UomId = 1,
                UomUnit = $"UomUnit{guid}",
                ProductTypeId = 1,
                ProductTypeCode = $"ProducTypeCode{guid}",
                ProductTypeName = $"ProducTypeName{guid}",
            };

            return model;
        }

        public async Task<GarmentLeftoverWarehouseProductModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
