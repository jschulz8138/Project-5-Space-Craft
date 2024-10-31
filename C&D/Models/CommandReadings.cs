namespace CAndD.Models
{
    public class CommandReadings
    {
        public string CommandType { get; set; }     // e.g., "AdjustPower"
        public string Target { get; set; }          // Target system or component
        public string Parameters { get; set; }      // Command parameters (e.g., "Power: 50%")
        public string Status { get; set; }          // e.g., "Pending", "Success", "Failed"

        public override string ToString()
        {
            return $"Command: {CommandType}, Target: {Target}, Parameters: {Parameters}, Status: {Status}";
        }
    }
}
