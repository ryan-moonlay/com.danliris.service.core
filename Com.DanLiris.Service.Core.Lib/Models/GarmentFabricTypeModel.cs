using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentFabricTypeModel : StandardEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
