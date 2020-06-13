using Com.DanLiris.Service.Core.Lib.Interfaces;
using Com.DanLiris.Service.Core.Lib.Models;
using System;
using System.Linq.Expressions;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentTransactionType
{
    public interface IGarmentTransactionTypeService : IBaseService<GarmentTransactionTypeModel>
    {
        bool CheckExisting(Expression<Func<GarmentTransactionTypeModel, bool>> filter);
    }
}
