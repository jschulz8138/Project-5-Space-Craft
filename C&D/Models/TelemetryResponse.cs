namespace CAndD.Models
{
    public class TelemetryResponse
    {
        public string Position { get; set; }
        public float Temperature { get; set; }
        public float Radiation { get; set; }
        public float Velocity { get; set; }

        public override string ToString()
        {
            return $"Position: {Position}, Temperature: {Temperature}°C, " +
                   $"Radiation: {Radiation} mSv, Velocity: {Velocity} m/s";
        }
    }
}
