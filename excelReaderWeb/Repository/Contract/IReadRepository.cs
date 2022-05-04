using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace excelReaderWeb.Repository.Contract
{
    public interface IReadRepository
    {
        Task<DataTable> GetEmployeeFromExcel(string fileName);
    }
}
