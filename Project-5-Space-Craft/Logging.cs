//Payload Ops
//File that handles logging to both the console and writing to an excel file
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Payload_Ops
{
    public static class Logging
    {
        private static String filename = "../../../LogFiles.xlsx";

        //Interacts with spaceship
        public static bool LogPacket(String packetType, String direction, String data)
        {
            if (logFile(packetType, direction, data, DateTime.Now) && logConsole(packetType, direction, data, DateTime.Now))
                return true;
            return false;
        }

        public static bool logFile(String type, String dir, String data, DateTime dt)
        {
            CheckAndCreateExcelFile(filename);
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
        public static bool CheckAndCreateExcelFile(string filePath)
        {
            if (File.Exists(filePath))
                return true;
            else
            {
                CreateNewExcelFile(filePath);
                return false;
            }
        }

        private static void CreateNewExcelFile(string filePath)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Sheet1"
                };
                sheets.Append(sheet);

                workbookPart.Workbook.Save();
            }
        }

        public static void InsertText(string docName, string text, string col, uint row)
        {
            using (var spreadSheet = SpreadsheetDocument.Open(docName, true))
            {
                var workbookPart = spreadSheet.WorkbookPart;
                if (workbookPart == null)
                    workbookPart = spreadSheet.AddWorkbookPart();

                var shareStringPart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault() ?? workbookPart.AddNewPart<SharedStringTablePart>();

                int index = InsertSharedStringItem(text, shareStringPart);
                var worksheetPart = workbookPart.WorksheetParts.First();
                var cell = InsertCellInWorksheet(col, row, worksheetPart);

                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = CellValues.SharedString;

                worksheetPart.Worksheet.Save();
            }
        }

        public static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            shareStringPart.SharedStringTable ??= new SharedStringTable();

            var existingItem = shareStringPart.SharedStringTable.Elements<SharedStringItem>().FirstOrDefault(item => item.InnerText == text);

            if (existingItem != null)
                return shareStringPart.SharedStringTable.Elements<SharedStringItem>().ToList().IndexOf(existingItem);

            var newItem = new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text));
            shareStringPart.SharedStringTable.AppendChild(newItem);
            shareStringPart.SharedStringTable.Save();

            return shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() - 1;
        }

        public static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex) ?? new Row() { RowIndex = rowIndex };

            if (row.RowIndex != rowIndex)
                sheetData.Append(row);

            var existingCell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
            if (existingCell != null)
                return existingCell;

            Cell refCell = row.Elements<Cell>().FirstOrDefault(c => string.Compare(c.CellReference?.Value, cellReference, true) > 0);

            var newCell = new Cell() { CellReference = cellReference };
            row.InsertBefore(newCell, refCell);
            worksheet.Save();

            return newCell;
        }


        public static uint GetNextEmptyCell(string fileName, string sheetName, string col)
        {
            uint rowIndex = 1;
            while (GetCellValue(fileName, sheetName, col + rowIndex) != string.Empty)
                rowIndex++;
            return rowIndex;
        }


        public static string GetCellValue(string fileName, string sheetName, string addressName)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, false))
            {
                var wbPart = document.WorkbookPart;
                var theSheet = wbPart?.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

                if (theSheet?.Id == null)
                    throw new ArgumentException($"Sheet '{sheetName}' not found.");

                var wsPart = (WorksheetPart)wbPart.GetPartById(theSheet.Id);
                var theCell = wsPart.Worksheet?.Descendants<Cell>().FirstOrDefault(c => c.CellReference == addressName);

                if (theCell == null || string.IsNullOrEmpty(theCell.InnerText))
                    return string.Empty;

                string value = theCell.InnerText;

                if (theCell.DataType == null)
                    return value;

                if (theCell.DataType.Value == CellValues.SharedString)
                {
                    var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    if (stringTable != null && int.TryParse(value, out int index))
                        value = stringTable.SharedStringTable.ElementAt(index).InnerText;
                }
                else if (theCell.DataType.Value == CellValues.Boolean)
                    value = value == "0" ? "FALSE" : "TRUE";

                return value;
            }
        }
    }
}

