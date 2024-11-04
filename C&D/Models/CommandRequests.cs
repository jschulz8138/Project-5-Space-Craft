namespace CAndD.Models
{
    public class CommandRequest
    {
        public string CommandType { get; set; }
        public string Target { get; set; }
        public string Parameters { get; set; }
    }
}
