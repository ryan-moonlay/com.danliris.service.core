using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentShippingStaff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentShippingStaffViewModel : BasicViewModel, IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentShippingStaffService service = (IGarmentShippingStaffService)validationContext.GetService(typeof(IGarmentShippingStaffService));

            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Name harus diisi", new List<string>() { "Name" });
            }
            else if (service.CheckExisting(d => d.Id != Id && d.Name == Name))
            {
                yield return new ValidationResult("Name sudah ada", new List<string>() { "Name" });
            }
        }
    }
}
