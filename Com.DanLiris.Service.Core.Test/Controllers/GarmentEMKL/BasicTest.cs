using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentEMKL;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentEMKL
{
    public class BasicTest : BaseControllerTest<GarmentEMKLController, GarmentEMKLModel, GarmentEMKLViewModel, IGarmentEMKLService>
    {
        public BasicTest()
        {
        }
    }
}
