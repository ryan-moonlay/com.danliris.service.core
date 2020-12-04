using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.BudgetingCategory
{
    public class BudgetingCategoryService : IBudgetingCategoryService
    {
        private const string UserAgent = "core-service";
        private readonly CoreDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IDistributedCache _cache;
        private readonly IServiceProvider _serviceProvider;

        public BudgetingCategoryService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<CoreDbContext>();
            _identityService = serviceProvider.GetService<IIdentityService>();
            _cache = serviceProvider.GetService<IDistributedCache>();

            _serviceProvider = serviceProvider;
        }

        private void SetCache()
        {
            var data = _dbContext.BudgetingCategories.ToList();
            _cache.SetString("BudgetingCategory", JsonConvert.SerializeObject(data));
        }

        private readonly List<string> _header = new List<string>()
        {
            "Kode", "Nama"
        };

        public List<string> CsvHeader => _header;

        public async Task<int> CreateModel(Models.BudgetingCategory model)
        {
            MoonlayEntityExtension.FlagForCreate(model, _identityService.Username, UserAgent);
            _dbContext.BudgetingCategories.Add(model);

            await _dbContext.SaveChangesAsync();
            SetCache();
            return model.Id;
        }

        public async Task<int> DeleteModel(int id)
        {
            var model = _dbContext.BudgetingCategories.FirstOrDefault(entity => entity.Id == id);
            MoonlayEntityExtension.FlagForDelete(model, _identityService.Username, UserAgent);
            _dbContext.BudgetingCategories.Update(model);

            await _dbContext.SaveChangesAsync();
            SetCache();
            return model.Id;
        }

        public ReadResponse<Models.BudgetingCategory> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}")
        {
            var query = _dbContext.BudgetingCategories.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<Models.BudgetingCategory>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<Models.BudgetingCategory>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<Models.BudgetingCategory>.Order(query, orderDictionary);

            var pageable = new Pageable<Models.BudgetingCategory>(query, page - 1, size);
            var data = pageable.Data.ToList();

            var totalData = pageable.TotalCount;
            SetCache();
            return new ReadResponse<Models.BudgetingCategory>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<Models.BudgetingCategory> ReadModelById(int id)
        {
            SetCache();
            return _dbContext.BudgetingCategories.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<int> UpdateModel(int id, Models.BudgetingCategory model)
        {
            var existingModel = _dbContext.BudgetingCategories.FirstOrDefault(entity => entity.Id == id);
            existingModel.Code = model.Code;
            existingModel.Name = model.Name;
            MoonlayEntityExtension.FlagForUpdate(existingModel, _identityService.Username, UserAgent);
            _dbContext.BudgetingCategories.Update(existingModel);

            await _dbContext.SaveChangesAsync();
            SetCache();
            return model.Id;
        }

        public Tuple<bool, List<object>> UploadValidate(List<Models.BudgetingCategory> data, List<KeyValuePair<string, StringValues>> body)
        {
            var errorList = new List<object>();
            var errorMessage = "";
            var valid = true;

            foreach (var categoryVM in data)
            {
                errorMessage = "";

                if (string.IsNullOrWhiteSpace(categoryVM.Code))
                {
                    errorMessage = string.Concat(errorMessage, "Kode tidak boleh kosong, ");
                }
                else if (data.Any(d => d != categoryVM && d.Code.Equals(categoryVM.Code)))
                {
                    errorMessage = string.Concat(errorMessage, "Kode tidak boleh duplikat, ");
                }

                if (string.IsNullOrWhiteSpace(categoryVM.Name))
                {
                    errorMessage = string.Concat(errorMessage, "Nama tidak boleh kosong, ");
                }
                else if (data.Any(d => d != categoryVM && d.Name.Equals(categoryVM.Name)))
                {
                    errorMessage = string.Concat(errorMessage, "Nama tidak boleh duplikat, ");
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    /* Service Validation */
                    if (_dbContext.AccountingCategories.Any(d => d._IsDeleted.Equals(false) && d.Code.Equals(categoryVM.Code)))
                    {
                        errorMessage = string.Concat(errorMessage, "Kode tidak boleh duplikat, ");
                    }

                    if (_dbContext.AccountingCategories.Any(d => d._IsDeleted.Equals(false) && d.Name.Equals(categoryVM.Name)))
                    {
                        errorMessage = string.Concat(errorMessage, "Nama tidak boleh duplikat, ");
                    }
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = errorMessage.Remove(errorMessage.Length - 2);
                    var Error = new ExpandoObject() as IDictionary<string, object>;

                    Error.Add("Kode", categoryVM.Code);
                    Error.Add("Nama", categoryVM.Name);
                    Error.Add("Error", errorMessage);

                    errorList.Add(Error);
                }
            }

            if (errorList.Count > 0)
            {
                valid = false;
            }

            return Tuple.Create(valid, errorList);
        }

        public Task<int> UploadData(List<Models.BudgetingCategory> data)
        {
            data = data.Select(element =>
            {
                MoonlayEntityExtension.FlagForCreate(element, _identityService.Username, UserAgent);
                return element;
            }).ToList();
            _dbContext.BudgetingCategories.AddRange(data);
            return _dbContext.SaveChangesAsync();
        }
    }

    public class BudgetingCategoryMap : ClassMap<Models.BudgetingCategory>
    {
        public BudgetingCategoryMap()
        {
            Map(b => b.Code).Index(0);
            Map(b => b.Name).Index(1);
        }
    }
}
