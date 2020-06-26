using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentTransactionType;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentTransactionType
{
    public class BasicTest : BaseControllerTest<GarmentTransactionTypeController, GarmentTransactionTypeModel, GarmentTransactionTypeViewModel, IGarmentTransactionTypeService>
    {
        public BasicTest()
        {
        }
    }
}
