using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using excelReaderWeb.Models;
using excelReaderWeb.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace excelReaderWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string fileName = @"C:\Users\fariz_izwan_ishak\Documents\Source Code\excelReaderWeb\sampleData.xlsx";
        private readonly IExcelService _excelService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IExcelService excelService, ILogger<WeatherForecastController> logger)
        {
            _excelService = excelService;
            _logger = logger;
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
