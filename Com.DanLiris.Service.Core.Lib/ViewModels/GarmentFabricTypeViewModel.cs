using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentFabricType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentFabricTypeViewModel : BasicViewModel, IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentFabricTypeService service = (IGarmentFabricTypeService)validationContext.GetService(typeof(IGarmentFabricTypeService));

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
