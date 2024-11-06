//Payload Ops
//File that handles logging to both the console and writing to an excel file
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Data;

namespace Project_5_Space_Craft
{
    public static class Logging
    {
        private static String filename = "../../../LogFiles.xlsx";

        //Interacts with PacketWrapper
        public static bool LogPacket(String packetType, String direction, String data)
        {
            logFile(packetType, direction, data, DateTime.Now);
            logConsole(packetType, direction, data, DateTime.Now);
            return true;
        }

        public static bool logFile(String type, String dir, String data, DateTime dt)
        {
            string time = dt.ToString("yyyy MMMM dd h:mm:ss tt");
            InsertText(filename, type, "A", GetNextEmptyCell(filename, "Sheet1", "A"));
            InsertText(filename, time, "B", GetNextEmptyCell(filename, "Sheet1", "B"));
            InsertText(filename, dir, "C", GetNextEmptyCell(filename, "Sheet1", "C"));
            InsertText(filename, data, "D", GetNextEmptyCell(filename, "Sheet1", "D"));
            return true;
        }

        public static bool logConsole(String type, String dir, String data, DateTime dt)
        {
            Console.WriteLine("Packet Detected!" +
                " DataType: " + type +
                " Time: " + dt +
                " Direction: " + dir +
                " Data: " + data);
            return true;
        }

        //Excel Helper Functions
        public static void InsertText(string docName, string text, string col, uint row)
        {
            using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(docName, true))
            {
                WorkbookPart workbookPart = spreadSheet.WorkbookPart ?? spreadSheet.AddWorkbookPart();
                SharedStringTablePart shareStringPart;
                if (workbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = workbookPart.AddNewPart<SharedStringTablePart>();
                }
                int index = InsertSharedStringItem(text, shareStringPart);
                Cell cell = InsertCellInWorksheet(col, row, workbookPart.WorksheetParts.First());
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
                workbookPart.WorksheetParts.First().Worksheet.Save();
            }
        }

        public static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            if (shareStringPart.SharedStringTable is null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }
            int i = 0;
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();
            return i;
        }

        public static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;
            Row row;
            if (sheetData?.Elements<Row>().Where(r => !(r.RowIndex is null || r.RowIndex != rowIndex)).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => !(r.RowIndex is null || r.RowIndex != rowIndex)).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }
            if (row.Elements<Cell>().Where(c => !(c.CellReference is null || c.CellReference.Value != columnName + rowIndex)).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => !(c.CellReference is null || c.CellReference.Value != cellReference)).First();
            }
            else
            {
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference?.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }
                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);
                worksheet.Save();
                return newCell;
            }
        }


        public static uint GetNextEmptyCell(string FileName, string sheetName, string col)
        {
            bool inLoop = true;
            uint rowIndex = 1;
            string cellReference;
            do
            {
                cellReference = col + rowIndex;
                if (GetCellValue(FileName, sheetName, cellReference) != string.Empty)
                {
                    rowIndex++;
                }
                else
                {
                    inLoop = false;
                }
            }
            while (inLoop);
            return rowIndex;
        }


        public static string GetCellValue(string fileName, string sheetName, string addressName)
        {
            string value = null;
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                Sheet theSheet = wbPart?.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();
                if (theSheet is null || theSheet.Id is null)
                {
                    throw new ArgumentException("sheetName");
                }
                WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(theSheet.Id);
                Cell theCell = wsPart.Worksheet?.Descendants<Cell>()?.Where(c => c.CellReference == addressName).FirstOrDefault();
                if (theCell is null || theCell.InnerText.Length < 0)
                {
                    return string.Empty;
                }
                value = theCell.InnerText;
                if (theCell.DataType is null)
                {
                    return value;
                }
                if (theCell.DataType.Value == CellValues.SharedString)
                {
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    if (!(stringTable is null))
                    {
                        value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                    }
                }
                else if (theCell.DataType.Value == CellValues.Boolean)
                {
                    switch (value)
                    {
                        case "0":
                            value = "FALSE";
                            break;
                        default:
                            value = "TRUE";
                            break;
                    }
                }
                return value;
            }
        }
    }
}

