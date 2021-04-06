using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class IBCurrencyService : IIBCurrencyService
    {
        private const string UserAgent = "core-service";
        private readonly CoreDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IServiceProvider _serviceProvider;

        public IBCurrencyService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<CoreDbContext>();
            _identityService = serviceProvider.GetService<IIdentityService>();
            _serviceProvider = serviceProvider;
        }

        public int Create(FormDto form)
        {
            if (_dbContext.IBCurrencies.Any(entity => entity.CurrencyId == form.CurrencyId && entity.Year == form.Year && entity.Month == form.Month))
            {
                var validationResult = new List<ValidationResult>
                {
                    new ValidationResult("Sudah Mata Uang dengan Tahun dan Bulan yang sama", new List<string>() { "Duplicate" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, validationResult);
            }

            var model = new IBCurrencyModel(form.CurrencyId, form.Year, form.Month, form.Rate);
            model.FlagForUpdate(_identityService.Username, UserAgent);
            _dbContext.IBCurrencies.Add(model);
            return _dbContext.SaveChanges();
        }

        public int Delete(IBCurrencyDto ibCurrency)
        {
            var model = _dbContext.IBCurrencies.FirstOrDefault(entity => entity.Id == ibCurrency.Id);
            model.FlagForDelete(_identityService.Username, UserAgent);
            _dbContext.IBCurrencies.Update(model);
            return _dbContext.SaveChanges();
        }

        public IndexDto Read(string keyword, int page, int size, string sort = "{}")
        {
            var query = from ibCurrency in _dbContext.IBCurrencies.AsQueryable()
                        join currency in _dbContext.Currencies.AsQueryable() on ibCurrency.CurrencyId equals currency.Id into ibCurrencies

                        from biCurrency in ibCurrencies.DefaultIfEmpty()

                        select new IBCurrencyDto(ibCurrency.Id, ibCurrency._LastModifiedUtc, ibCurrency.Year, ibCurrency.Month, ibCurrency.Rate, biCurrency);

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(entity => entity.Currency.Code.Contains(keyword));

            var sortDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(sort);
            query = Order(query, sortDictionary);

            var total = query.Count();

            var data = query.Skip((page - 1) * size).Take(size).ToList();
            return new IndexDto(data, total);
        }

        private IQueryable<IBCurrencyDto> Order(IQueryable<IBCurrencyDto> query, Dictionary<string, string> sortDictionary)
        {
            /* Default Order */
            if (sortDictionary.Count.Equals(0))
            {
                sortDictionary.Add("_LastModifiedUtc", "desc");

                query = query.OrderByDescending(b => b._LastModifiedUtc);
            }
            /* Custom Order */
            else
            {
                var key = sortDictionary.Keys.First();
                var orderType = sortDictionary[key];

                query = query.OrderBy(string.Concat(key.Replace(".", ""), " ", orderType));
            }
            return query;
        }

        public IBCurrencyDto Read(int id)
        {
            var model = _dbContext.IBCurrencies.FirstOrDefault(entity => entity.Id == id);
            if (model == null)
                return null;

            var currency = _dbContext.Currencies.FirstOrDefault(entity => entity.Id == model.CurrencyId);

            return new IBCurrencyDto(model.Id, model._LastModifiedUtc, model.Year, model.Month, model.Rate, currency);
        }

        public int Update(IBCurrencyDto ibCurrency, FormDto form)
        {
            var model = _dbContext.IBCurrencies.FirstOrDefault(entity => entity.Id == ibCurrency.Id);
            model.UpdateRate(form.Rate);
            model.FlagForUpdate(_identityService.Username, UserAgent);
            _dbContext.IBCurrencies.Update(model);
            return _dbContext.SaveChanges();
        }

        public string GenerateCSVTemplate()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Mata Uang,Tahun,Bulan,Rate");
            return builder.ToString();
        }

        public CSVResult UploadCSV(List<CSVFormDto> data)
        {
            var result = new CSVResult();

            var currencyCodes = data.Select(element => element.Code).ToList();
            var currencies = _dbContext.Currencies.Where(entity => currencyCodes.Contains(entity.Code)).ToList();

            if (data.Count <= 0)
            {
                result.ErrorValidation();
            }

            var models = new List<IBCurrencyModel>();
            foreach (var datum in data)
            {
                var currency = currencies.FirstOrDefault(element => element.Code == datum.Code);
                if (currency != null)
                {
                    if (datum.GetYear() <= 0)
                    {
                        datum.ErrorMessage += "tahun tidak valid\n";
                        result.ErrorValidation();
                    }

                    if (datum.GetMonth() <= 0)
                    {
                        datum.ErrorMessage += "bulan tidak valid\n";
                        result.ErrorValidation();
                    }

                    if (datum.GetRate() <= 0)
                    {
                        datum.ErrorMessage += "rate harus lebih besar dari 0\n";
                        result.ErrorValidation();
                    }
                }
                else
                {
                    datum.ErrorMessage += "mata uang tidak valid atau belum terdaftar\n";
                    result.ErrorValidation();
                }
            }

            if (!result.IsAnyValidationError)
            {
                foreach (var datum in data)
                {
                    var currency = currencies.FirstOrDefault(element => element.Code == datum.Code);
                    var model = new IBCurrencyModel(currency.Id, datum.GetYear(), datum.GetMonth(), datum.GetRate());
                    model.FlagForUpdate(_identityService.Username, UserAgent);
                    _dbContext.IBCurrencies.Add(model);
                }
                _dbContext.SaveChanges();
            }

            return result;
        }
    }
}
