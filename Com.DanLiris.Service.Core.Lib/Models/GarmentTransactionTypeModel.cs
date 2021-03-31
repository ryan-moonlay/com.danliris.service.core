using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class GarmentTransactionTypeModel : StandardEntity
    {
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int? COAId { get; set; }

        [StringLength(50)]
        public string COACode { get; set; }

        [StringLength(50)]
        public string COAName { get; set; }
    }
}
