using Com.DanLiris.Service.Core.Lib.Services;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Models
{
    public class ProcessType : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Unit { get; set; }
        
        public string SPPCode { get; set; }

        /*order type*/
        public int OrderTypeId { get; set; }
        public string OrderTypeCode { get; set; }
        public string OrderTypeName { get; set; }
        public string OrderTypeRemark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResult = new List<ValidationResult>();
            /* Service Validation */
            ProcessTypeService service = (ProcessTypeService)validationContext.GetService(typeof(ProcessTypeService));
            if (string.IsNullOrWhiteSpace(this.Name))
                validationResult.Add(new ValidationResult("Name is required", new List<string> { "Name" }));
            else
            {
                if (service.DbContext.Set<ProcessType>().Count(r => r._IsDeleted.Equals(false) && r.Id != this.Id && r.Name.Equals(this.Name)) > 0) /* Name Unique */
                    validationResult.Add(new ValidationResult("Name already exists", new List<string> { "Name" }));
            }

            if (string.IsNullOrWhiteSpace(this.Code))
                validationResult.Add(new ValidationResult("Code is required", new List<string> { "Code" }));
            else
            {
                if (service.DbContext.Set<ProcessType>().Count(r => r._IsDeleted.Equals(false) && r.Id != this.Id && r.Code.Equals(this.Code)) > 0) /* Code Unique */
                    validationResult.Add(new ValidationResult("Code already exists", new List<string> { "Code" }));
            }

            if (string.IsNullOrWhiteSpace(this.Unit))
                validationResult.Add(new ValidationResult("Unit is required", new List<string> { "Unit" }));

            if (string.IsNullOrWhiteSpace(this.SPPCode))
                validationResult.Add(new ValidationResult("Kode SPP is required", new List<string> { "SPPCode" }));


            return validationResult;
        }
    }
}
