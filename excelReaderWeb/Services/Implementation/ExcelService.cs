using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using excelReaderWeb.Models;
using excelReaderWeb.Repository.Contract;
using excelReaderWeb.Services.Contract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace excelReaderWeb.Services.Implementation
{
    public class ExcelService : IExcelService
    {
        private readonly ILogger<ExcelService> _logger;
        private readonly IReadRepository _readRepository;
        public ExcelService(IReadRepository readRepository, ILogger<ExcelService> logger)
        {
            _logger = logger;
            _readRepository = readRepository;
        }

        public async Task<EmployeeResponse> ReadExcel(string fileName)
        {
            EmployeeResponse emRes = new EmployeeResponse();
            try
            {
                var result = await _readRepository.GetEmployeeFromExcel(fileName);
                if (result != null)
                {
                    emRes.StatusCode = HttpStatusCode.OK;
                    emRes.Employee = result.AsEnumerable().Select(row =>
                    new Employee
                    {
                        EmployeeNumber = int.Parse(row.Field<string>("EmployeeNumber")),
                        FirstName = row.Field<string>("FirstName"),
                        LastName = row.Field<string>("LastName"),
                        EmployeeStatus = row.Field<string>("EmployeeStatus")
                    }).ToList();
                }
                else
                {
                    emRes.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                emRes.StatusCode = HttpStatusCode.InternalServerError;
            }

            return emRes;
        }
    }
}
