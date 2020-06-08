using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentFabricType;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentFabricType
{
    public class BasicTest : BaseControllerTest<GarmentFabricTypeController, GarmentFabricTypeModel, GarmentFabricTypeViewModel, IGarmentFabricTypeService>
    {
        public BasicTest()
        {
        }
    }
}
