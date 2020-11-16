using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Linq.Expressions;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentAdditionalCharges
{
    public interface IGarmentAdditionalChargesService : IBaseService<GarmentAdditionalChargesModel>
    {
        bool CheckExisting(Expression<Func<GarmentAdditionalChargesModel, bool>> filter);
    }
}
