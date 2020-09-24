using Com.DanLiris.Service.Core.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentCategoryViewModel : BasicViewModel
	{
        public string code { get; set; }
        public string name { get; set; }
        public string codeRequirement { get; set; }
        public string categoryType { get; set; }
        public GarmentCategoryUomViewModel UOM { get; set; }
    }

    public class GarmentCategoryUomViewModel
    {
        public int? Id { get; set; }
        public string Unit { get; set; }
    }
}
