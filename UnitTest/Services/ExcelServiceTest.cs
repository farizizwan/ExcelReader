using excelReaderWeb.Models;
using excelReaderWeb.Repository.Contract;
using excelReaderWeb.Services.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controller
{
    public class ExcelServiceTest
    {
        private ExcelService _excelService;
        private Mock<IReadRepository> _mockRepository;
        private Mock<ILogger<ExcelService>> _mockLogger;

        public ExcelServiceTest()
        {
            _mockRepository = new Mock<IReadRepository>();
            _mockLogger = new Mock<ILogger<ExcelService>>();
            _excelService = new ExcelService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ReadExcel_Success()
        {
            string fileName = "test.xlsx";
            DataTable dtTable;
            dtTable = MakeEmployeeTable_PassScenario();

            DataRow row;
            row = dtTable.NewRow();

            row["EmployeeNumber"] = "1";
            row["FirstName"] = "John";
            row["LastName"] = "Doe";
            row["EmployeeStatus"] = "Regular";
            dtTable.Rows.Add(row);

            _mockRepository.Setup(x => x.GetEmployeeFromExcel(It.IsAny<string>())).Returns(Task.FromResult(dtTable));

            var response = await _excelService.ReadExcel(fileName);
            var result = Assert.IsType<EmployeeResponse>(response);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Employee);
        }

        [Fact]
        public async Task ReadExcel_Fail()
        {
            string fileName = "test.xlsx";
            DataTable dtTable;
            dtTable = MakeEmployeeTable_FailedScenario();

            DataRow row;
            row = dtTable.NewRow();

            row["EmployeeNumber"] = "1";
            row["FirstName"] = "John";
            row["LastName"] = "Doe";
            dtTable.Rows.Add(row);

            _mockRepository.Setup(x => x.GetEmployeeFromExcel(It.IsAny<string>())).Returns(Task.FromResult(dtTable));

            var response = await _excelService.ReadExcel(fileName);
            var result = Assert.IsType<EmployeeResponse>(response);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Employee);
        }

        [Fact]
        public async Task ReadExcel_Null()
        {
            string fileName = "test.xlsx";
            DataTable dtTable = null;

            _mockRepository.Setup(x => x.GetEmployeeFromExcel(It.IsAny<string>())).Returns(Task.FromResult(dtTable));

            var response = await _excelService.ReadExcel(fileName);
            var result = Assert.IsType<EmployeeResponse>(response);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.Null(result.Employee);
        }

        private DataTable MakeEmployeeTable_PassScenario()
        {
            DataTable namesTable = new DataTable("Employee");

            DataColumn idColumn = new DataColumn();
            idColumn.DataType = System.Type.GetType("System.String");
            idColumn.ColumnName = "EmployeeNumber";
            namesTable.Columns.Add(idColumn);

            DataColumn fNameColumn = new DataColumn();
            fNameColumn.DataType = System.Type.GetType("System.String");
            fNameColumn.ColumnName = "FirstName";
            namesTable.Columns.Add(fNameColumn);

            DataColumn lNameColumn = new DataColumn();
            lNameColumn.DataType = System.Type.GetType("System.String");
            lNameColumn.ColumnName = "LastName";
            namesTable.Columns.Add(lNameColumn);

            DataColumn Status = new DataColumn();
            Status.DataType = System.Type.GetType("System.String");
            Status.ColumnName = "EmployeeStatus";
            namesTable.Columns.Add(Status);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = fNameColumn;
            namesTable.PrimaryKey = keys;

            return namesTable;
        }

        private DataTable MakeEmployeeTable_FailedScenario()
        {
            DataTable namesTable = new DataTable("Employee");

            DataColumn idColumn = new DataColumn();
            idColumn.DataType = System.Type.GetType("System.Int32");
            idColumn.AutoIncrement = true;
            idColumn.ColumnName = "EmployeeNumber";
            namesTable.Columns.Add(idColumn);

            DataColumn fNameColumn = new DataColumn();
            fNameColumn.DataType = System.Type.GetType("System.String");
            fNameColumn.ColumnName = "FirstName";
            namesTable.Columns.Add(fNameColumn);

            DataColumn lNameColumn = new DataColumn();
            lNameColumn.DataType = System.Type.GetType("System.String");
            lNameColumn.ColumnName = "LastName";
            namesTable.Columns.Add(lNameColumn);

            DataColumn Status = new DataColumn();
            Status.DataType = System.Type.GetType("System.String");
            Status.ColumnName = "EmployeeStatus";
            namesTable.Columns.Add(Status);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = fNameColumn;
            namesTable.PrimaryKey = keys;

            return namesTable;
        }
    }
}
