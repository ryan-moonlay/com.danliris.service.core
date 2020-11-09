using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.BICurrency
{
    public class BICurrencyService : IBICurrencyService
    {
        private const string UserAgent = "core-service";
        private readonly CoreDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IServiceProvider _serviceProvider;

        public BICurrencyService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<CoreDbContext>();
            _identityService = serviceProvider.GetService<IIdentityService>();

            _serviceProvider = serviceProvider;
        }

        private readonly List<string> _header = new List<string>()
        {
            "Code", "Name", "Date", "Rate"
        };

        public List<string> CsvHeader => _header;

        public Task<int> CreateModel(Models.BICurrency model)
        {
            if (_dbContext.BICurrencies.Any(entity => entity.Code == model.Code && entity.Date.Date == model.Date.Date))
            {
                var errorResult = new List<ValidationResult>()
                    {
                        new ValidationResult("Data with Code and same Date already exists", new List<string> { "Duplicate" })
                    };
                ValidationContext validationContext = new ValidationContext(model, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }
            MoonlayEntityExtension.FlagForCreate(model, _identityService.Username, UserAgent);
            _dbContext.BICurrencies.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteModel(int id)
        {
            var model = _dbContext.BICurrencies.FirstOrDefault(entity => entity.Id == id);
            MoonlayEntityExtension.FlagForDelete(model, _identityService.Username, UserAgent);
            _dbContext.BICurrencies.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public ReadResponse<Models.BICurrency> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}")
        {
            var query = _dbContext.BICurrencies.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<Models.BICurrency>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<Models.BICurrency>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<Models.BICurrency>.Order(query, orderDictionary);

            var pageable = new Pageable<Models.BICurrency>(query, page - 1, size);
            var data = pageable.Data.ToList();

            var totalData = pageable.TotalCount;
            return new ReadResponse<Models.BICurrency>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<Models.BICurrency> ReadModelById(int id)
        {
            return _dbContext.BICurrencies.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateModel(int id, Models.BICurrency model)
        {
            if (_dbContext.BICurrencies.Any(entity => entity.Code == model.Code && entity.Date.Date == model.Date.Date))
            {
                var errorResult = new List<ValidationResult>()
                    {
                        new ValidationResult("Data with Code and same Date already exists", new List<string> { "Duplicate" })
                    };
                ValidationContext validationContext = new ValidationContext(model, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            var existingModel = _dbContext.BICurrencies.FirstOrDefault(entity => entity.Id == id);
            existingModel.Code = model.Code;
            existingModel.Date = model.Date;
            existingModel.Rate = model.Rate;
            MoonlayEntityExtension.FlagForUpdate(existingModel, _identityService.Username, UserAgent);
            _dbContext.BICurrencies.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public Tuple<bool, List<object>> UploadValidate(List<Models.BICurrency> data, List<KeyValuePair<string, StringValues>> body)
        {
            var errorList = new List<object>();
            var errorMessage = "";
            var valid = true;
            var dbData = _dbContext.BICurrencies.ToList();
            foreach (var datum in data)
            {
                errorMessage = "";

                if (string.IsNullOrWhiteSpace(datum.Code))
                {
                    errorMessage = string.Concat(errorMessage, "Code tidak boleh kosong, ");
                }

                if (datum.Date > DateTime.Now)
                {
                    errorMessage = string.Concat(errorMessage, "Tanggal tidak boleh kosong, ");
                }

                if (datum.Rate.Equals(null) || datum.Rate < 0)
                {
                    errorMessage = string.Concat(errorMessage, "Rate tidak boleh kosong, ");
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = errorMessage.Remove(errorMessage.Length - 2);
                    var error = new ExpandoObject() as IDictionary<string, object>;

                    error.Add("Code", datum.Code);
                    error.Add("Date", datum.Date);
                    error.Add("Rate", datum.Rate);
                    error.Add("Error", errorMessage);

                    errorList.Add(error);
                }
            }

            if (errorList.Count > 0)
            {
                valid = false;
            }

            return Tuple.Create(valid, errorList);
        }

        public Task<int> UploadData(List<Models.BICurrency> data)
        {
            data = data.Select(element =>
            {
                MoonlayEntityExtension.FlagForCreate(element, _identityService.Username, UserAgent);
                return element;
            }).ToList();
            _dbContext.BICurrencies.AddRange(data);
            return _dbContext.SaveChangesAsync();
        }
    }

    public class BICurrencyMap : ClassMap<Models.BICurrency>
    {
        public BICurrencyMap()
        {
            Map(b => b.Code).Index(0);
            Map(b => b.Name).Index(1);
            Map(b => b.Date).Index(2);
            Map(b => b.Rate).Index(3);
        }
    }
}
