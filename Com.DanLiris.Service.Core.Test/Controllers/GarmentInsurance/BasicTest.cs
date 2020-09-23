using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentInsurance;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.Test.Utils;
using Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers;

namespace Com.DanLiris.Service.Core.Test.Controllers.GarmentInsurance
{
    public class BasicTest : BaseControllerTest<GarmentInsuranceController, GarmentInsuranceModel, GarmentInsuranceViewModel, IGarmentInsuranceService>
    {
        public BasicTest()
        {
        }
    }
}
