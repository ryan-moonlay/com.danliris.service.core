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

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentShippingStaff
{
    public class GarmentShipingStaffService : IGarmentShippingStaffService
    {
        private const string _UserAgent = "core-service";
        protected DbSet<GarmentShippingStaffModel> _DbSet;
        protected IIdentityService _IdentityService;
        public CoreDbContext _DbContext;

        public GarmentShipingStaffService(IServiceProvider serviceProvider, CoreDbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = dbContext.Set<GarmentShippingStaffModel>();
            _IdentityService = serviceProvider.GetService<IIdentityService>();
        }

        public async Task<int> CreateAsync(GarmentShippingStaffModel model)
        {
            model.FlagForCreate(_IdentityService.Username, _UserAgent);
            model._LastModifiedAgent = model._CreatedAgent;
            model._LastModifiedBy = model._CreatedBy;
            model._LastModifiedUtc = model._CreatedUtc;

            _DbSet.Add(model);
            return await _DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentShippingStaffModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentShippingStaffModel> Query = _DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Name"
            };
            Query = QueryHelper<GarmentShippingStaffModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentShippingStaffModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentShippingStaffModel>.Order(Query, OrderDictionary);

            Query = Query.Select(s => new GarmentShippingStaffModel
            {
                Id = s.Id,
                Name = s.Name,
            });

            Pageable<GarmentShippingStaffModel> pageable = new Pageable<GarmentShippingStaffModel>(Query, page - 1, size);
            List<GarmentShippingStaffModel> Data = pageable.Data.ToList();

            int TotalData = pageable.TotalCount;
            return new ReadResponse<GarmentShippingStaffModel>(Data, TotalData, OrderDictionary, new List<string>());
        }

        public async Task<GarmentShippingStaffModel> ReadByIdAsync(int id)
        {
            return await _DbSet.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(int id, GarmentShippingStaffModel model)
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

        public bool CheckExisting(Expression<Func<GarmentShippingStaffModel, bool>> filter)
        {
            var count = _DbSet.Where(filter).Count();

            return count > 0;
        }
    }
}
