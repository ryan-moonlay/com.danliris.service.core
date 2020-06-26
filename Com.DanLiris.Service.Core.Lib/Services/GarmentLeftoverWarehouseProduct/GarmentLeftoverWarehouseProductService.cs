using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseProduct
{
    public class GarmentLeftoverWarehouseProductService : IGarmentLeftoverWarehouseProductService
    {
        private const string _UserAgent = "core-service";
        protected DbSet<GarmentLeftoverWarehouseProductModel> _DbSet;
        protected IIdentityService _IdentityService;
        public CoreDbContext _DbContext;

        public GarmentLeftoverWarehouseProductService(IServiceProvider serviceProvider, CoreDbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = dbContext.Set<GarmentLeftoverWarehouseProductModel>();
            _IdentityService = serviceProvider.GetService<IIdentityService>();
        }

        public async Task<int> CreateAsync(GarmentLeftoverWarehouseProductModel model)
        {
            model.FlagForCreate(_IdentityService.Username, _UserAgent);
            model._LastModifiedAgent = model._CreatedAgent;
            model._LastModifiedBy = model._CreatedBy;
            model._LastModifiedUtc = model._CreatedUtc;

            _DbSet.Add(model);
            return await _DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentLeftoverWarehouseProductModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentLeftoverWarehouseProductModel> Query = _DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "Name", "UomUnit","ProductTypeCode", "ProductTypeName"
            };
            Query = QueryHelper<GarmentLeftoverWarehouseProductModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentLeftoverWarehouseProductModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentLeftoverWarehouseProductModel>.Order(Query, OrderDictionary);

            Query = Query.Select(s => new GarmentLeftoverWarehouseProductModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                UomId = s.UomId,
                UomUnit = s.UomUnit,
                ProductTypeId = s.ProductTypeId,
                ProductTypeCode = s.ProductTypeCode,
                ProductTypeName = s.ProductTypeName,
            });

            Pageable<GarmentLeftoverWarehouseProductModel> pageable = new Pageable<GarmentLeftoverWarehouseProductModel>(Query, page - 1, size);
            List<GarmentLeftoverWarehouseProductModel> Data = pageable.Data.ToList();

            int TotalData = pageable.TotalCount;
            return new ReadResponse<GarmentLeftoverWarehouseProductModel>(Data, TotalData, OrderDictionary, new List<string>());
        }

        public async Task<GarmentLeftoverWarehouseProductModel> ReadByIdAsync(int id)
        {
            return await _DbSet.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(int id, GarmentLeftoverWarehouseProductModel model)
        {
            model.FlagForUpdate(_IdentityService.Username, _UserAgent);
            _DbSet.Update(model);
            return await _DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var model = _DbSet.Where(w => w.Id == id).FirstOrDefault();
            model.FlagForDelete(_IdentityService.Username, _UserAgent);
            _DbSet.Update(model);
            return await _DbContext.SaveChangesAsync();
        }

        public bool CheckExisting(Expression<Func<GarmentLeftoverWarehouseProductModel, bool>> filter)
        {
            var count = _DbSet.Where(filter).Count();

            return count > 0;
        }
    }
}
