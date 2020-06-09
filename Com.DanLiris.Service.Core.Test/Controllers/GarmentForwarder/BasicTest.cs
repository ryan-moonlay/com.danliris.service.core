using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentForwarder;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentForwarder
{
    public class BasicTest : BaseControllerTest<GarmentForwarderController, GarmentForwarderModel, GarmentForwarderViewModel, IGarmentForwarderService>
    {
        public BasicTest()
        {
        }
    }
}
