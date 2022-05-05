using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using excelReaderWeb.Controllers;
using excelReaderWeb.Models;
using excelReaderWeb.Repository.Implementation;
using excelReaderWeb.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controller
{
    public class ReadRespositoryTest
    {
        private ReadRepository _readRepo;
        private Mock<ILogger<ReadRepository>> _mockLogger;

        public ReadRespositoryTest()
        {
            _mockLogger = new Mock<ILogger<ReadRepository>>();
            _readRepo = new ReadRepository(_mockLogger.Object);
        }

        [Fact]
        public async Task GetEmployeeFromExcel_Fail()
        {
            
            string fileName = @"test";
            var response = await _readRepo.GetEmployeeFromExcel(fileName);
            var result = Assert.IsType<DataTable>(response);
            Assert.Empty(result.Rows);
        }

        [Fact]
        public async Task GetEmployeeFromExcel_Success()
        {
            string fileName = @"..\..\..\..\sampleData.xlsx";
            var response = await _readRepo.GetEmployeeFromExcel(fileName);
            var result = Assert.IsType<DataTable>(response);
            Assert.NotEmpty(result.Rows);
        }

    }
}
