using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentCourier;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentCourier
{
    public class BasicTest : BaseControllerTest<GarmentCourierController, GarmentCourierModel, GarmentCourierViewModel, IGarmentCourierService>
    {
        public BasicTest()
        {
        }
    }
}
