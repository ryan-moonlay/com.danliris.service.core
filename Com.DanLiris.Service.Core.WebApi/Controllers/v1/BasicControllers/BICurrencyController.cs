using Com.DanLiris.Service.Core.Lib.Helpers;
using Com.DanLiris.Service.Core.Lib.Helpers.IdentityService;
using Com.DanLiris.Service.Core.Lib.Helpers.ValidateService;
using Com.DanLiris.Service.Core.Lib.Models;
using Com.DanLiris.Service.Core.Lib.Services.BICurrency;
using Com.DanLiris.Service.Core.Lib.Services.IBCurrency;
using Com.DanLiris.Service.Core.WebApi.Utils;
using Com.Moonlay.NetCore.Lib.Service;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using General = Com.DanLiris.Service.Core.WebApi.Helpers.General;

namespace Com.DanLiris.Service.Core.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/master/bi-currencies")]
    [Authorize]
    public class BICurrencyController : Controller
    {
        private const string ContentType = "application/vnd.openxmlformats";
        private readonly string FileName = string.Concat("Error Log - ", typeof(BICurrency).Name, " ", DateTime.Now.ToString("dd MMM yyyy"), ".csv");
        private const string ApiVersion = "1.0.0";
        private readonly IIdentityService _identityService;
        private readonly IValidateService _validateService;
        private readonly IIBCurrencyService _service;

        public BICurrencyController(IServiceProvider serviceProvider)
        {
            _identityService = serviceProvider.GetService<IIdentityService>();
            _validateService = serviceProvider.GetService<IValidateService>();
            _service = serviceProvider.GetService<IIBCurrencyService>();
        }

        private void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet]
        public IActionResult Get(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")] List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                var result = _service.Read(keyword, page, size, order);
                return Ok(new
                {
                    apiVersion = ApiVersion,
                    statusCode = General.OK_STATUS_CODE,
                    message = General.OK_MESSAGE,
                    data = result.Data,
                    info = new
                    {
                        total = result.Total,
                        page,
                        size,
                        count = result.Data.Count
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, e.Message + " " + e.StackTrace);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] FormDto form)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(form);

                _service.Create(form);

                var response = new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE).Ok();
                return Created(string.Concat(Request.Path, "/", 0), response);
            }
            catch (ServiceValidationException e)
            {
                var response = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE).Fail(e);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, response);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var model = _service.Read(id);

                if (model == null)
                {
                    var response = new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE).Fail();
                    return NotFound(response);
                }
                else
                {
                    var response = new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE).Ok(null, model);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, response);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] FormDto form)
        {
            try
            {
                VerifyUser();
                _validateService.Validate(form);

                var ibCurrency = _service.Read(id);

                if (ibCurrency == null)
                {
                    var response = new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE).Fail();
                    return NotFound();
                }

                _service.Update(ibCurrency, form);

                return NoContent();
            }
            catch (ServiceValidationException e)
            {
                var response = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE).Fail(e);
                return BadRequest(response);
            }
            catch (Exception e)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, response);
            }
        }

        [HttpGet("generate-template")]
        public IActionResult GenerateTemplate()
        {
            try
            {
                var template = _service.GenerateCSVTemplate();
                return File(Encoding.UTF8.GetBytes(template), "text/csv", "mata-uang-bi.csv");
            }
            catch (Exception e)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, response);
            }
        }

        private readonly List<string> CSVHeaders = new List<string>() { "Mata Uang", "Tahun", "Bulan", "Rate" };

        [HttpPost("imports")]
        public IActionResult Import(IFormFile file)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                //if (file.Length > 0)
                {
                    VerifyUser();
                    var uploadedFile = Request.Form.Files[0];
                    var reader = new StreamReader(uploadedFile.OpenReadStream());
                    var fileHeader = new List<string>(reader.ReadLine().Split(","));
                    var validHeader = CSVHeaders.SequenceEqual(fileHeader, StringComparer.OrdinalIgnoreCase);

                    if (validHeader)
                    {
                        reader.DiscardBufferedData();
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        reader.BaseStream.Position = 0;
                        var csv = new CsvReader(reader);
                        csv.Configuration.IgnoreQuotes = false;
                        csv.Configuration.Delimiter = ",";
                        csv.Configuration.RegisterClassMap<IBCurrencyMap>();
                        csv.Configuration.HeaderValidated = null;
                        csv.Configuration.MissingFieldFound = null;

                        var data = csv.GetRecords<CSVFormDto>().ToList();

                        var result = _service.UploadCSV(data);

                        reader.Close();

                        if (!result.IsAnyValidationError) /* If Data Valid */
                        {
                            var response = new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE).Ok();
                            return Created(HttpContext.Request.Path, response);

                        }
                        else
                        {
                            var error = result.GetDataStringWithErrorMessage();
                            return File(Encoding.UTF8.GetBytes(error), "text/csv", "mata-uang-bi.csv");
                        }
                    }
                    else
                    {
                        var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, General.CSV_ERROR_MESSAGE).Fail();

                        return NotFound(response);
                    }
                }
                else
                {
                    var response = new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.NO_FILE_ERROR_MESSAGE).Fail();
                    return BadRequest(response);
                }
            }
            catch (TypeConverterException ex)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, "Tahun, Bulan, Rate diisi huruf\n" + ex.Message).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
            catch (Exception e)
            {
                var response = new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message).Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, response);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();
                var model = _service.Read(id);

                if (model == null)
                {
                    var response = new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE).Fail();
                    return NotFound(response);
                }

                _service.Delete(model);
                return NoContent();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }

    public class IBCurrencyMap : ClassMap<CSVFormDto>
    {
        public IBCurrencyMap()
        {
            Map(b => b.Code).Index(0);
            Map(b => b.Year).Index(1);
            Map(b => b.Month).Index(2);
            Map(b => b.Rate).Index(3);
        }
    }
}
