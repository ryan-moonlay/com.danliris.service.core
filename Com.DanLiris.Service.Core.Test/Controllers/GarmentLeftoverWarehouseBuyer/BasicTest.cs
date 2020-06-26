using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseBuyer;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentLeftoverWarehouseBuyer
{
    public class BasicTest : BaseControllerTest<GarmentLeftoverWarehouseBuyerController, GarmentLeftoverWarehouseBuyerModel, GarmentLeftoverWarehouseBuyerViewModel, IGarmentLeftoverWarehouseBuyerService>
    {
        public BasicTest()
        {
        }
    }
}
