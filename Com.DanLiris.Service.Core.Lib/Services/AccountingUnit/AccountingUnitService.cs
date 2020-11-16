using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.AccountingUnit
{
    public class AccountingUnitService : IAccountingUnitService
    {
        private const string UserAgent = "core-service";
        private readonly CoreDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IServiceProvider _serviceProvider;

        public AccountingUnitService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetService<CoreDbContext>();
            _identityService = serviceProvider.GetService<IIdentityService>();

            _serviceProvider = serviceProvider;
        }

        private readonly List<string> _header = new List<string>()
        {
            "Code", "Divisi", "Nama", "Deskripsi"
        };

        public List<string> CsvHeader => _header;

        public Task<int> CreateModel(Models.AccountingUnit model)
        {
            MoonlayEntityExtension.FlagForCreate(model, _identityService.Username, UserAgent);
            _dbContext.AccountingUnits.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteModel(int id)
        {
            var model = _dbContext.AccountingUnits.FirstOrDefault(entity => entity.Id == id);
            MoonlayEntityExtension.FlagForDelete(model, _identityService.Username, UserAgent);
            _dbContext.AccountingUnits.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public ReadResponse<Models.AccountingUnit> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}")
        {
            var query = _dbContext.AccountingUnits.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<Models.AccountingUnit>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<Models.AccountingUnit>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<Models.AccountingUnit>.Order(query, orderDictionary);

            var pageable = new Pageable<Models.AccountingUnit>(query, page - 1, size);
            var data = pageable.Data.ToList();

            var totalData = pageable.TotalCount;
            return new ReadResponse<Models.AccountingUnit>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<Models.AccountingUnit> ReadModelById(int id)
        {
            return _dbContext.AccountingUnits.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateModel(int id, Models.AccountingUnit model)
        {
            var existingModel = _dbContext.AccountingUnits.FirstOrDefault(entity => entity.Id == id);
            existingModel.Code = model.Code;
            existingModel.Description = model.Description;
            existingModel.DivisionCode = model.DivisionCode;
            existingModel.DivisionId = model.DivisionId;
            existingModel.DivisionName = model.DivisionName;
            MoonlayEntityExtension.FlagForUpdate(existingModel, _identityService.Username, UserAgent);
            _dbContext.AccountingUnits.Update(existingModel);
            return _dbContext.SaveChangesAsync();
        }

        public Tuple<bool, List<object>> UploadValidate(List<Models.AccountingUnit> data, List<KeyValuePair<string, StringValues>> body)
        {
            var errorList = new List<object>();
            var errorMessage = "";
            var valid = true;
            Division division = null;

            foreach (Models.AccountingUnit unitVM in data)
            {
                errorMessage = "";

                if (string.IsNullOrWhiteSpace(unitVM.Code))
                {
                    errorMessage = string.Concat(errorMessage, "Kode tidak boleh kosong, ");
                }
                else if (data.Any(d => d != unitVM && d.Code.Equals(unitVM.Code)))
                {
                    errorMessage = string.Concat(errorMessage, "Kode tidak boleh duplikat, ");
                }

                if (string.IsNullOrWhiteSpace(unitVM.Name))
                {
                    errorMessage = string.Concat(errorMessage, "Nama tidak boleh kosong, ");
                }
                else if (data.Any(d => d != unitVM && d.Name.Equals(unitVM.Name)))
                {
                    errorMessage = string.Concat(errorMessage, "Nama tidak boleh duplikat, ");
                }

                if (string.IsNullOrWhiteSpace(unitVM.DivisionName))
                {
                    errorMessage = string.Concat(errorMessage, "Divisi tidak boleh kosong, ");
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    /* Service Validation */
                    division = _dbContext.Divisions.FirstOrDefault(d => d._IsDeleted.Equals(false) && d.Name.Equals(unitVM.DivisionName));

                    if (_dbContext.AccountingUnits.Any(d => d._IsDeleted.Equals(false) && d.Code.Equals(unitVM.Code)))
                    {
                        errorMessage = string.Concat(errorMessage, "Kode tidak boleh duplikat, ");
                    }

                    if (_dbContext.AccountingUnits.Any(d => d._IsDeleted.Equals(false) && d.Name.Equals(unitVM.Name)))
                    {
                        errorMessage = string.Concat(errorMessage, "Nama tidak boleh duplikat, ");
                    }

                    if (division == null)
                    {
                        errorMessage = string.Concat(errorMessage, "Divisi tidak terdaftar di Master Divisi, ");
                    }
                }

                if (string.IsNullOrEmpty(errorMessage))
                {
                    unitVM.DivisionId = division.Id;
                    unitVM.DivisionCode = division.Code;
                }
                else
                {
                    errorMessage = errorMessage.Remove(errorMessage.Length - 2);
                    var Error = new ExpandoObject() as IDictionary<string, object>;

                    Error.Add("Kode Unit", unitVM.Code);
                    Error.Add("Divisi", unitVM.Name);
                    Error.Add("Nama", unitVM.Name);
                    Error.Add("Deskripsi", unitVM.Description);
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

        public Task<int> UploadData(List<Models.AccountingUnit> data)
        {
            data = data.Select(element =>
            {
                MoonlayEntityExtension.FlagForCreate(element, _identityService.Username, UserAgent);
                return element;
            }).ToList();
            _dbContext.AccountingUnits.AddRange(data);
            return _dbContext.SaveChangesAsync();
        }
    }

    public class AccountingUnitMap : ClassMap<Models.AccountingUnit>
    {
        public AccountingUnitMap()
        {
            Map(b => b.Code).Index(0);
            Map(b => b.DivisionName).Index(1);
            Map(b => b.Name).Index(2);
            Map(b => b.Description).Index(3);
        }
    }
}
