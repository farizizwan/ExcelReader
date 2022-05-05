using excelReaderWeb.Controllers;
using excelReaderWeb.Models;
using excelReaderWeb.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controller
{
    public class ExcelControllerTest
    {
        private ExcelController _excelController;
        private Mock<IExcelService> _mockExcelService;

        public ExcelControllerTest()
        {
            _mockExcelService = new Mock<IExcelService>();
            _excelController = new ExcelController(_mockExcelService.Object);
        }

        [Fact]
        public async Task GetExcel_Success()
        {
            List<Employee> _mockEmployee = new List<Employee>(){
               new Employee {
                    EmployeeNumber = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    EmployeeStatus = "Regular"
                },
               new Employee
               {
                   EmployeeNumber = 2,
                    FirstName = "Harry",
                    LastName = "Potter",
                    EmployeeStatus = "Contractor"
               }
            };
            EmployeeResponse _mockEmployeeResponse = new EmployeeResponse
            {
                StatusCode = HttpStatusCode.OK,
                Employee = _mockEmployee
            };
            _mockExcelService.Setup(x => x.ReadExcel(It.IsAny<string>())).Returns(Task.FromResult(_mockEmployeeResponse));

            var response = await _excelController.Get();
            var result = Assert.IsType<JsonResult>(response);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetExcel_Fail()
        {
            List<Employee> _mockEmployee = new List<Employee>(){};
            EmployeeResponse _mockEmployeeResponse = new EmployeeResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Employee = _mockEmployee
            };
            _mockExcelService.Setup(x => x.ReadExcel(It.IsAny<string>())).Returns(Task.FromResult(_mockEmployeeResponse));

            var response = await _excelController.Get();
            var result = Assert.IsType<JsonResult>(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
