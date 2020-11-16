using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentLeftoverWarehouseBuyerModel : StandardEntity
    {
        [MaxLength(25)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(3000)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string NIK { get; set; }

        [MaxLength(50)]
        public string NPWP { get; set; }

        [MaxLength(50)]
        public string WPName { get; set; }

        [MaxLength(20)]
        public string KaberType { get; set; }
    }
}
