using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentLeftoverWarehouseComodityModel : StandardEntity
    {
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
