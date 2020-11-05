using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.AccountingUnit
{
    public interface IAccountingUnitService : IBasicUploadCsvService<Models.AccountingUnit>
    {
        ReadResponse<Models.AccountingUnit> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}");
        Task<int> CreateModel(Models.AccountingUnit model);
        Task<int> UpdateModel(int Id, Models.AccountingUnit model);
        Task<Models.AccountingUnit> ReadModelById(int id);
        Task<int> DeleteModel(int id);
        Task<int> UploadData(List<Models.AccountingUnit> data);
    }
}
