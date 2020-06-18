using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services;
using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentLeftoverWarehouseProductModel : StandardEntity
    {
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? UomId { get; set; }
        [StringLength(50)]
        public string UomUnit { get; set; }
        public int? ProductTypeId { get; set; }
        [StringLength(50)]
        public string ProductTypeCode { get; set; }
        [StringLength(50)]
        public string ProductTypeName { get; set; }
    }
}
