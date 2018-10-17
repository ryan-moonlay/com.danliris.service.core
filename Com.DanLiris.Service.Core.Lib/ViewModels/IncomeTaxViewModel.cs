﻿using Com.DanLiris.Service.Core.Lib.Helpers;
using System;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class IncomeTaxViewModel : BasicViewModel
    {
        public string code { get; set; }

        public string name { get; set; }

        /* Double */
        public double? rate { get; set; }

        public string description { get; set; }
    }
}
