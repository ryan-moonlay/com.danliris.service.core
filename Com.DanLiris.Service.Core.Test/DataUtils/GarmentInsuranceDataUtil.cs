using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentInsurance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class GarmentInsuranceDataUtil
    {
        private readonly GarmentInsuranceService Service;

        public GarmentInsuranceDataUtil(GarmentInsuranceService service)
        {
            Service = service;
        }

        public GarmentInsuranceModel GetNewData()
        {
            Guid guid = Guid.NewGuid();

            GarmentInsuranceModel model = new GarmentInsuranceModel
            {
                Code = $"Code{guid}",
                Name = $"Name{guid}",
                Address = $"Address{guid}",
                Attention = $"Attention{guid}",
                PhoneNumber = $"PhoneNumber{guid}",
                BankName = $"BankName{guid}",
                AccountNumber = $"AccountNumber{guid}",
                SwiftCode = $"SwiftCode{guid}",
                NPWP = $"NPWP{guid}",
            };

            return model;
        }

        public async Task<GarmentInsuranceModel> GetTestData()
        {
            var data = GetNewData();

            await Service.CreateAsync(data);

            return data;
        }
    }
}
