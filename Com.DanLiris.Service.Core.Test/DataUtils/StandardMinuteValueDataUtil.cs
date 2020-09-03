using Com.DanLiris.Service.Core.Lib;
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
    public class StandardMinuteValueDataUtil : BasicDataUtil<CoreDbContext, StandardMinuteValueService, StandardMinuteValue>, IEmptyData<StandardMinuteValueViewModel>
    {
        public StandardMinuteValueDataUtil(CoreDbContext dbContext, StandardMinuteValueService service) : base(dbContext, service)
        {
        }

        public StandardMinuteValueViewModel GetEmptyData()
        {
            return new StandardMinuteValueViewModel()
            {
                BuyerCode="",
                BuyerName="",
                ComodityCode="",
                ComodityName="",
                SMVSewing=1
            };
        }

        public override StandardMinuteValue GetNewData()
        {
            return new StandardMinuteValue()
            {
                BuyerName= "BuyerName",
                ComodityName= "ComodityName",
                ComodityCode= "ComodityCode",
                SMVCutting=1,
                SMVSewing =1,
                SMVFinishing=1
            };
        }

        public  override async Task<StandardMinuteValue> GetTestDataAsync()
        {
            var data = GetNewData();
            await Service.CreateModel(data);
            return data;
        }
    }
}
