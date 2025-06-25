using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class SensorDataDTO
    { 
        public int FingerID { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? Timestamp { get; set; }

    }
}
