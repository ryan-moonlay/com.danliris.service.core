using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentLeftoverWarehouseComodity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentLeftoverWarehouseComodityViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentLeftoverWarehouseComodityService service = (IGarmentLeftoverWarehouseComodityService)validationContext.GetService(typeof(IGarmentLeftoverWarehouseComodityService));

            if (string.IsNullOrWhiteSpace(Code))
            {
                yield return new ValidationResult("Kode Komoditi harus diisi", new List<string>() { "Code" });
            }
            else if (service.CheckExisting(d => d.Id != Id && d.Code == Code))
            {
                yield return new ValidationResult("Kode Komoditi sudah ada", new List<string>() { "Code" });
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Nama Komoditi harus diisi", new List<string>() { "Name" });
            }
            else if (service.CheckExisting(d => d.Id != Id && d.Name == Name))
            {
                yield return new ValidationResult("Nama Komoditi sudah ada", new List<string>() { "Name" });
            }

        }
    }
}
