using excelReaderWeb.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Get()
        {
            var response =  await _excelService.ReadExcel(fileName);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            { 
                return new JsonResult(response) { StatusCode = (int)response.StatusCode, Value = response.Employee };
            }
            else
            {
                return new JsonResult(response) { StatusCode = (int)response.StatusCode};
            }

        }

    }
}
