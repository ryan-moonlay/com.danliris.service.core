using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentTransactionType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentTransactionTypeDataUtil
    {
        private readonly GarmentTransactionTypeService Service;

        public GarmentTransactionTypeDataUtil(GarmentTransactionTypeService service)
        {
            Service = service;
        }

        public GarmentTransactionTypeModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentTransactionTypeModel model = new GarmentTransactionTypeModel
            {
                Code = $"Code{guid}",
                Name = $"Name{guid}",
                COAId = 1,
                COACode = $"COACode{guid}",
                COAName = $"COAName{guid}",
            };

            return model;
        }

        public async Task<GarmentTransactionTypeModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
