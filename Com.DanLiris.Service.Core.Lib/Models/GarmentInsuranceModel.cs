using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentInsuranceModel : StandardEntity
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
        public string BankName { get; set; }

        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        public string SwiftCode { get; set; }

        [MaxLength(50)]
        public string NPWP { get; set; }
    }
}
