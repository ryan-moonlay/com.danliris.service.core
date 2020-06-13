using AutoMapper;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.GarmentTransactionType;
using Com.DanLiris.Service.Core.Lib.ViewModels;
using Com.DanLiris.Service.Core.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/master/garment-transaction-types")]
    [Authorize]
    public class GarmentTransactionTypeController : BaseController< GarmentTransactionTypeModel, GarmentTransactionTypeViewModel, IGarmentTransactionTypeService>
    {
        public GarmentTransactionTypeController(IIdentityService identityService, IValidateService validateService, IGarmentTransactionTypeService service, IMapper mapper) : base(identityService, validateService, service, mapper, "1.0.0")
        {
        }
    }
}
