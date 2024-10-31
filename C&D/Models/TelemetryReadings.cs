namespace CAndD.Models
{
    public class TelemetryReadings
    {
        public string Position { get; set; }       // e.g., "X:100, Y:200, Z:300"
        public float Temperature { get; set; }     // e.g., temperature in Celsius
        public float Radiation { get; set; }       // e.g., radiation level
        public float Velocity { get; set; }        // e.g., speed of the spacecraft

        public override string ToString()
        {
            return $"Position: {Position}, Temperature: {Temperature}°C, " +
                   $"Radiation: {Radiation} mSv, Velocity: {Velocity} m/s";
        }
    }
}
