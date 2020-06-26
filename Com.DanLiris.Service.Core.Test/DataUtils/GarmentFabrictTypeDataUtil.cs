using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentFabricType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentFabricTypeDataUtil
    {
        private readonly GarmentFabricTypeService Service;

        public GarmentFabricTypeDataUtil(GarmentFabricTypeService service)
        {
            Service = service;
        }

        public GarmentFabricTypeModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentFabricTypeModel model = new GarmentFabricTypeModel
            {
                Name = $"Name{guid}",
            };

            return model;
        }

        public async Task<GarmentFabricTypeModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
