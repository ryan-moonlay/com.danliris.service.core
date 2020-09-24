using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentCourierModel : StandardEntity
    {
        [MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(3000)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Attention { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }
                
        [MaxLength(50)]
        public string FaxNumber { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string NPWP { get; set; }
    }
}
