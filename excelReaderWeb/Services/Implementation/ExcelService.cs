﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using excelReaderWeb.Models;
using excelReaderWeb.Services.Contract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace excelReaderWeb.Services.Implementation
{
    public class ExcelService : IExcelService
    {
        private readonly ILogger<ExcelService> _logger;
        public ExcelService(ILogger<ExcelService> logger)
        {
            _logger = logger;
        }

        public string ReadExcelasJSON(string fileName)
        {
            try
            {
                DataTable dtTable = new DataTable();
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fileName, false))
                { 
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();

                    foreach (Sheet thesheet in thesheetcollection.OfType<Sheet>())
                    {
                        Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;
                        SheetData thesheetdata = theWorksheet.GetFirstChild<SheetData>();

                        for (int rCnt = 0; rCnt < thesheetdata.ChildElements.Count(); rCnt++)
                        {
                            List<string> rowList = new List<string>();
                            for (int rCnt1 = 0; rCnt1
                                < thesheetdata.ElementAt(rCnt).ChildElements.Count(); rCnt1++)
                            {

                                Cell thecurrentcell = (Cell)thesheetdata.ElementAt(rCnt).ChildElements.ElementAt(rCnt1);
                                //statement to take the integer value  
                                string currentcellvalue = string.Empty;
                                if (thecurrentcell.DataType != null)
                                {
                                    if (thecurrentcell.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                //first row will provide the column name.
                                                if (rCnt == 0)
                                                {
                                                    dtTable.Columns.Add(item.Text.Text.Replace(" ",string.Empty));
                                                }
                                                else
                                                {
                                                    rowList.Add(item.Text.Text);
                                                }
                                            }
                                            else if (item.InnerText != null)
                                            {
                                                currentcellvalue = item.InnerText;
                                            }
                                            else if (item.InnerXml != null)
                                            {
                                                currentcellvalue = item.InnerXml;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (rCnt != 0)//reserved for column values
                                    {
                                        rowList.Add(thecurrentcell.InnerText);
                                    }
                                }
                            }
                            if (rCnt != 0)//reserved for column values
                                dtTable.Rows.Add(rowList.ToArray());

                        }

                    }
                    return JsonConvert.SerializeObject(dtTable);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
