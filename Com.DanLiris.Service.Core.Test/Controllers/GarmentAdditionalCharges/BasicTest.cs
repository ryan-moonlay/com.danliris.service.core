using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentAdditionalCharges;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentAdditionalCharges
{
    public class BasicTest : BaseControllerTest<GarmentAdditionalChargesController, GarmentAdditionalChargesModel, GarmentAdditionalChargesViewModel, IGarmentAdditionalChargesService>
    {
        public BasicTest()
        {
        }
    }
}
