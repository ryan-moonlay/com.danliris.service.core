using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class IBCurrencyModel : StandardEntity
    {
        public IBCurrencyModel()
        {

        }

        public IBCurrencyModel(int currencyId, int year, int month, double rate)
        {
            CurrencyId = currencyId;
            Year = year;
            Month = month;
            Rate = rate;
        }

        public int CurrencyId { get; private set; }
        public int Year { get; private set; }
        public int Month { get; private set; }
        public double Rate { get; private set; }

        public void UpdateRate(double rate)
        {
            Rate = rate;
        }
    }
}
