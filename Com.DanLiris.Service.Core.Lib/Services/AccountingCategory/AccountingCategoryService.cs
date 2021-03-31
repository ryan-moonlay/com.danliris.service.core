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

namespace Com.DanLiris.Service.Core.Lib.Services.AccountingCategory
{
    public class AccountingCategoryService : IAccountingCategoryService
    {
        private const string UserAgent = "core-service";
        private readonly CoreDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IDistributedCache _cache;
        private readonly IServiceProvider _serviceProvider;

        public AccountingCategoryService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<CoreDbContext>();
            _identityService = serviceProvider.GetService<IIdentityService>();
            _cache = serviceProvider.GetService<IDistributedCache>();

            _serviceProvider = serviceProvider;
        }

        private void SetCache()
        {
            var data = _dbContext.AccountingCategories.ToList();
            _cache.SetString("AccountingCategory", JsonConvert.SerializeObject(data));
        }

        private readonly List<string> _header = new List<string>()
        {
            "Kode", "Nama"
        };

        public List<string> CsvHeader => _header;

        public Task<int> CreateModel(Models.AccountingCategory model)
        {
            MoonlayEntityExtension.FlagForCreate(model, _identityService.Username, UserAgent);
            _dbContext.AccountingCategories.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteModel(int id)
        {
            var model = _dbContext.AccountingCategories.FirstOrDefault(entity => entity.Id == id);
            MoonlayEntityExtension.FlagForDelete(model, _identityService.Username, UserAgent);
            _dbContext.AccountingCategories.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public ReadResponse<Models.AccountingCategory> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}")
        {
            var query = _dbContext.AccountingCategories.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<Models.AccountingCategory>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<Models.AccountingCategory>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<Models.AccountingCategory>.Order(query, orderDictionary);

            var pageable = new Pageable<Models.AccountingCategory>(query, page - 1, size);
            var data = pageable.Data.ToList();

            var totalData = pageable.TotalCount;
            return new ReadResponse<Models.AccountingCategory>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<Models.AccountingCategory> ReadModelById(int id)
        {
            return _dbContext.AccountingCategories.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateModel(int id, Models.AccountingCategory model)
        {
            var existingModel = _dbContext.AccountingCategories.FirstOrDefault(entity => entity.Id == id);
            existingModel.Code = model.Code;
            existingModel.Name = model.Name;
            MoonlayEntityExtension.FlagForUpdate(existingModel, _identityService.Username, UserAgent);
            _dbContext.AccountingCategories.Update(existingModel);
            return _dbContext.SaveChangesAsync();
        }

        public Tuple<bool, List<object>> UploadValidate(List<Models.AccountingCategory> data, List<KeyValuePair<string, StringValues>> body)
        {
            var errorList = new List<object>();
            var errorMessage = "";
            var valid = true;

            foreach (Models.AccountingCategory categoryVM in data)
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

        public Task<int> UploadData(List<Models.AccountingCategory> data)
        {
            data = data.Select(element =>
            {
                MoonlayEntityExtension.FlagForCreate(element, _identityService.Username, UserAgent);
                return element;
            }).ToList();
            _dbContext.AccountingCategories.AddRange(data);
            return _dbContext.SaveChangesAsync();
        }
    }

    public class AccountingCategoryMap : ClassMap<Models.AccountingCategory>
    {
        public AccountingCategoryMap()
        {
            Map(b => b.Code).Index(0);
            Map(b => b.Name).Index(1);
        }
    }
}
