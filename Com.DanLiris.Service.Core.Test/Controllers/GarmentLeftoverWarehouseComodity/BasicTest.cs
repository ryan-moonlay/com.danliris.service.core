using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseComodity;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentLeftoverWarehouseComodity
{
    public class BasicTest : BaseControllerTest<GarmentLeftoverWarehouseComodityController, GarmentLeftoverWarehouseComodityModel, GarmentLeftoverWarehouseComodityViewModel, IGarmentLeftoverWarehouseComodityService>
    {
        public BasicTest()
        {
        }
    }
}
