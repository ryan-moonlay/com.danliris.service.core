using Com.DanLiris.Service.Core.Lib.Models;
using System;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class IBCurrencyDto
    {
        public IBCurrencyDto(int id, DateTime lastModifiedUtc, int year, int month, double rate, Currency currency)
        {
            Id = id;
            _LastModifiedUtc = lastModifiedUtc;
            Year = year;
            Month = month;
            Rate = rate;
            Currency = new CurrencyDto(currency.Id, currency.Code);
        }

        public int Id { get; private set; }
        public DateTime _LastModifiedUtc { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public double Rate { get; private set; }
        public CurrencyDto Currency { get; private set; }
    }
}