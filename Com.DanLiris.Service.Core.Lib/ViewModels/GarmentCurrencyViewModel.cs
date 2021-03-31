using Com.DanLiris.Service.Core.Lib.Helpers;
using System;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentCurrencyViewModel : BasicViewModel
    {

        public string code { get; set; }
        public string Code { get; set; }

        public DateTime date { get; set; }
        public DateTime Date { get; set; }

        /* Double */
        public dynamic rate { get; set; }
        public dynamic Rate { get; set; }
    }
}
