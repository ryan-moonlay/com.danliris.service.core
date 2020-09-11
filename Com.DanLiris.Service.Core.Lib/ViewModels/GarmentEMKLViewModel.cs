using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentEMKL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentEMKLViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Attention { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string NPWP { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentEMKLService service = (IGarmentEMKLService)validationContext.GetService(typeof(IGarmentEMKLService));

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
        }
    }
}
