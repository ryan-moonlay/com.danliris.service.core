using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Linq.Expressions;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseProduct
{
    public interface IGarmentLeftoverWarehouseProductService : IBaseService<GarmentLeftoverWarehouseProductModel>
    {
        bool CheckExisting(Expression<Func<GarmentLeftoverWarehouseProductModel, bool>> filter);
    }
}
