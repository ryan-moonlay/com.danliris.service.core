using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.Core.Lib.Services.IBCurrency
{
    public class CSVResult
    {
        public CSVResult()
        {
            Data = new List<CSVFormDto>();
        }

        public List<CSVFormDto> Data { get; private set; }
        public bool IsAnyValidationError { get; private set; }

        public void ErrorValidation()
        {
            IsAnyValidationError = true;
        }

        public string GetDataStringWithErrorMessage()
        {
            var builder = new StringBuilder();

            if (Data.Count <= 0)
            {
                builder.AppendLine("Mata Uang,Tahun,Bulan,Rate,Error");
                builder.AppendLine("dat kosong (hapus row ini dan tambah data baru)");
            }
            else
                builder.AppendLine("Mata Uang,Tahun,Bulan,Rate,Error");

            foreach (var datum in Data)
            {
                builder.AppendLine($"{datum.Code},{datum.Year},{datum.Month},{datum.Rate},{datum.ErrorMessage}");
            }

            return builder.ToString();
        }
    }
}