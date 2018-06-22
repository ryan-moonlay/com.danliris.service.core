﻿using Com.DanLiris.Service.Core.Lib;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Helpers;
using Com.DanLiris.Service.Core.Test.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Test.DataUtils
{
    public class IncomeTaxDataUtil : BasicDataUtil<CoreDbContext, IncomeTaxService, IncomeTax>, IEmptyData<IncomeTaxViewModel>
    {

        public IncomeTaxDataUtil(CoreDbContext dbContext, IncomeTaxService service) : base(dbContext, service)
        {
        }

        public IncomeTaxViewModel GetEmptyData()
        {
            IncomeTaxViewModel Data = new IncomeTaxViewModel();

            Data.name = "";
            Data.rate = null;
            Data.code = "";
            Data.description = "";
            return Data;
        }

        public override IncomeTax GetNewData()
        {
            string guid = Guid.NewGuid().ToString();
            IncomeTax TestData = new IncomeTax
            {
                Name = "TEST",
                Rate = 1.5,
                Description="test",
                
            };

            return TestData;
        }

        public override async Task<IncomeTax> GetTestDataAsync()
        {
            IncomeTax Data = GetNewData();
            await this.Service.CreateModel(Data);
            return Data;
        }
    }
}