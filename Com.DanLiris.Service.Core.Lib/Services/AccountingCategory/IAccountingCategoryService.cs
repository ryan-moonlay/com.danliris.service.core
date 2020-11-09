using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.AccountingCategory
{
    public interface IAccountingCategoryService : IBasicUploadCsvService<Models.AccountingCategory>
    {
        ReadResponse<Models.AccountingCategory> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}");
        Task<int> CreateModel(Models.AccountingCategory model);
        Task<int> UpdateModel(int Id, Models.AccountingCategory model);
        Task<Models.AccountingCategory> ReadModelById(int id);
        Task<int> DeleteModel(int id);
        Task<int> UploadData(List<Models.AccountingCategory> data);
    }
}
