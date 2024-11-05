using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CAndD.Models;
using OfficeOpenXml; // Namespace for EPPlus library

namespace CAndD.Services
{
    public class LogService
    {
        private readonly string _logFilePath;

        public LogService()
        {
            string solutionRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _logFilePath = Path.Combine(solutionRootPath, "TelemetryLog.xlsx");

            // Initialize the log file if it doesn't exist
            if (!File.Exists(_logFilePath))
            {
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("TelemetryLog");

                // Add headers
                worksheet.Cells[1, 1].Value = "Timestamp";
                worksheet.Cells[1, 2].Value = "Position";
                worksheet.Cells[1, 3].Value = "Temperature";
                worksheet.Cells[1, 4].Value = "Radiation";
                worksheet.Cells[1, 5].Value = "Velocity";

                package.SaveAs(new FileInfo(_logFilePath));
            }
        }

        public void LogTelemetryData(TelemetryResponse telemetryData)
        {
            using var package = new ExcelPackage(new FileInfo(_logFilePath));
            var worksheet = package.Workbook.Worksheets["TelemetryLog"];
            int newRow = worksheet.Dimension.End.Row + 1;

            // Write data to the new row
            worksheet.Cells[newRow, 1].Value = telemetryData.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            worksheet.Cells[newRow, 2].Value = telemetryData.Position;
            worksheet.Cells[newRow, 3].Value = telemetryData.Temperature;
            worksheet.Cells[newRow, 4].Value = telemetryData.Radiation;
            worksheet.Cells[newRow, 5].Value = telemetryData.Velocity;

            package.Save();
            Console.WriteLine($"Telemetry data logged to Excel at {_logFilePath}");
        }
    }
}
