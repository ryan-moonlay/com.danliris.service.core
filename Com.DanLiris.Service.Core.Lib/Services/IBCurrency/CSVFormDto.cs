using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class CSVFormDto
    {
        public string Code { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Rate { get; set; }
        public string ErrorMessage { get; set; }

        public int GetYear()
        {
            int.TryParse(Year, out var year);
            return year;
        }

        public int GetMonth()
        {
            int.TryParse(Month, out var month);
            return month;
        }

        public double GetRate()
        {
            double.TryParse(Rate, out var rate);
            return rate;
        }
    }
}
