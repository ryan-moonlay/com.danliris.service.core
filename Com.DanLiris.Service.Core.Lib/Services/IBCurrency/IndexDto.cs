using System.Collections.Generic;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class IndexDto
    {
        public IndexDto(List<IBCurrencyDto> data, int total)
        {
            Data = data;
            Total = total;
        }

        public List<IBCurrencyDto> Data { get; private set; }
        public int Total { get; private set; }
    }
}