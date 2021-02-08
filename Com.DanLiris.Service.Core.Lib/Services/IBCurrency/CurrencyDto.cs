using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class CurrencyDto
    {
        public CurrencyDto(int id, string code)
        {
            Id = id;
            Code = code;
        }

        public int Id { get; private set; }
        public string Code { get; private set; }
    }
}
