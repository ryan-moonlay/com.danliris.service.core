using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Linq.Expressions;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentEMKL
{
    public interface IGarmentEMKLService : IBaseService<GarmentEMKLModel>
    {
        bool CheckExisting(Expression<Func<GarmentEMKLModel, bool>> filter);
    }
}
