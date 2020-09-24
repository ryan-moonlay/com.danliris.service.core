using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Linq.Expressions;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentCourier
{
    public interface IGarmentCourierService : IBaseService<GarmentCourierModel>
    {
        bool CheckExisting(Expression<Func<GarmentCourierModel, bool>> filter);
    }
}
