using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class BICurrency : StandardEntity, IValidatableObject
    {
        [StringLength(32)]
        public string Code { get; set; }
        [StringLength(128)]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double? Rate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(Code))
                validationResult.Add(new ValidationResult("Code is required", new List<string> { "Code" }));

            if (Date > DateTime.Now)
                validationResult.Add(new ValidationResult("Date must be less than or equal today's date", new List<string> { "Date" }));

            if (Rate.Equals(null) || Rate < 0)
                validationResult.Add(new ValidationResult("Rate must be greater than zero", new List<string> { "Rate" }));

            return validationResult;
        }
    }
}
