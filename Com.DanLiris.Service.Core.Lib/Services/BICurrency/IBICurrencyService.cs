using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.Core.Lib.Services.BICurrency
{
    public interface IBICurrencyService : IBasicUploadCsvService<Models.BICurrency>
    {
        ReadResponse<Models.BICurrency> ReadModel(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}");
        Task<int> CreateModel(Models.BICurrency model);
        Task<int> UpdateModel(int Id, Models.BICurrency model);
        Task<Models.BICurrency> ReadModelById(int id);
        Task<int> DeleteModel(int id);
        Task<int> UploadData(List<Models.BICurrency> data);
    }
}
