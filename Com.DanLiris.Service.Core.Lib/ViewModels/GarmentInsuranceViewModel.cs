using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Services.GarmentInsurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.ViewModels
{
    public class GarmentInsuranceViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Attention { get; set; }

        public string PhoneNumber { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string SwiftCode { get; set; }

        public string NPWP { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGarmentInsuranceService service = (IGarmentInsuranceService)validationContext.GetService(typeof(IGarmentInsuranceService));

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
