using excelReaderWeb.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace excelReaderWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private static readonly string fileName = @"..\sampleData.xlsx";
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response =  _excelService.ReadExcelasJSON(fileName);
            if (!String.IsNullOrEmpty(response))
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

    }
}
