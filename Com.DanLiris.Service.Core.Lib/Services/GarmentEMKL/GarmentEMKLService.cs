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

namespace Com.DanLiris.Service.Core.Lib.Services.GarmentEMKL
{
    public class GarmentEMKLService : IGarmentEMKLService
    {
        private const string _UserAgent = "core-service";
        protected DbSet<GarmentEMKLModel> _DbSet;
        protected IIdentityService _IdentityService;
        public CoreDbContext _DbContext;

        public GarmentEMKLService(IServiceProvider serviceProvider, CoreDbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = dbContext.Set<GarmentEMKLModel>();
            _IdentityService = serviceProvider.GetService<IIdentityService>();
        }

        public async Task<int> CreateAsync(GarmentEMKLModel model)
        {
            model.FlagForCreate(_IdentityService.Username, _UserAgent);
            model._LastModifiedAgent = model._CreatedAgent;
            model._LastModifiedBy = model._CreatedBy;
            model._LastModifiedUtc = model._CreatedUtc;

            _DbSet.Add(model);
            return await _DbContext.SaveChangesAsync();
        }

        public ReadResponse<GarmentEMKLModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentEMKLModel> Query = _DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "Name", "Address", "Attention", "PhoneNumber", "FaxNumber", "Email", "NPWP"
            };
            Query = QueryHelper<GarmentEMKLModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentEMKLModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentEMKLModel>.Order(Query, OrderDictionary);

            Query = Query.Select(s => new GarmentEMKLModel
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Address = s.Address,
                Attention = s.Attention,
                PhoneNumber = s.PhoneNumber,
                FaxNumber = s.FaxNumber,
                Email = s.Email, 
                NPWP = s.NPWP,
            });

            Pageable<GarmentEMKLModel> pageable = new Pageable<GarmentEMKLModel>(Query, page - 1, size);
            List<GarmentEMKLModel> Data = pageable.Data.ToList();

            int TotalData = pageable.TotalCount;
            return new ReadResponse<GarmentEMKLModel>(Data, TotalData, OrderDictionary, new List<string>());
        }

        public async Task<GarmentEMKLModel> ReadByIdAsync(int id)
        {
            return await _DbSet.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(int id, GarmentEMKLModel model)
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

        public bool CheckExisting(Expression<Func<GarmentEMKLModel, bool>> filter)
        {
            var count = _DbSet.Where(filter).Count();

            return count > 0;
        }
    }
}
