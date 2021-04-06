using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseComodity
{
    public interface IGarmentLeftoverWarehouseComodityService : IBaseService<GarmentLeftoverWarehouseComodityModel>
    {
        bool CheckExisting(Expression<Func<GarmentLeftoverWarehouseComodityModel, bool>> filter);
    }
}
