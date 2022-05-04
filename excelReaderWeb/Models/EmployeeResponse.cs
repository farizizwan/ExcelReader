using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace excelReaderWeb.Models
{
    public class EmployeeResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<Employee> Employee { get; set; }
    }
}
