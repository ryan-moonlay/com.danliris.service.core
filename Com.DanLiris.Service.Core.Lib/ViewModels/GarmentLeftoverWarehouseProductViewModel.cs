using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentLeftoverWarehouseProductViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int? UomId { get; set; }

        public string UomUnit { get; set; }
        public int? ProductTypeId { get; set; }

        public string ProductTypeCode { get; set; }

        public string ProductTypeName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentLeftoverWarehouseProductService service = (IGarmentLeftoverWarehouseProductService)validationContext.GetService(typeof(IGarmentLeftoverWarehouseProductService));

            if (string.IsNullOrWhiteSpace(Code))
            {
                yield return new ValidationResult("Kode harus diisi", new List<string>() { "Code" });
            }
            else if (service.CheckExisting(d => d.Id != Id && d.Code == Code))
            {
                yield return new ValidationResult("Kode sudah ada", new List<string>() { "Code" });
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Nama harus diisi", new List<string>() { "Name" });
            }
            else if (service.CheckExisting(d => d.Id != Id && d.Name == Name))
            {
                yield return new ValidationResult("Nama sudah ada", new List<string>() { "Name" });
            }

            if (string.IsNullOrWhiteSpace(UomUnit))
            {
                yield return new ValidationResult("Nama harus diisi", new List<string>() { "UomUnit" });
            }

            if (string.IsNullOrWhiteSpace(ProductTypeCode))
            {
                yield return new ValidationResult("Jenis Barang harus diisi", new List<string>() { "ProductTypeCode" });
            }

        }
    }
}
