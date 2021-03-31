using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class FormDto : IValidatableObject
    {
        public int CurrencyId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public double Rate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();

            if (CurrencyId <= 0)
                validationResult.Add(new ValidationResult("Currency harus diisi", new List<string> { "CurrencyId" }));

            if (Year <= 0)
                validationResult.Add(new ValidationResult("Tahun harus dipilih", new List<string> { "Year" }));

            if (Month <= 0)
                validationResult.Add(new ValidationResult("Bulan harus dipilih", new List<string> { "Month" }));

            if (Rate <= 0)
                validationResult.Add(new ValidationResult("Rate harus lebih besar dari 0", new List<string> { "Rate" }));

            return validationResult;
        }
    }
}