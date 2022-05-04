using excelReaderWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace excelReaderWeb.Services.Contract
{
    public interface IExcelService
    {
        Task<EmployeeResponse> ReadExcel(string fileName);
    }
}
