using Newtonsoft.Json;

namespace WpfApp.Base.Models
{
    internal class TimeZoneData
    {
        [JsonProperty("abbreviation")]
        public DateTime? Abbreviation { get; set; }

        [JsonProperty("client_ip")]
        public string? ClientIp { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("day_of_week")]
        public int DayOfWeek { get; set; }

        [JsonProperty("day_of_year")]
        public int DayOfYear { get; set; }

        [JsonProperty("week_number")]
        public int WeekNumber { get; set; }

        [JsonProperty("dst")]
        public bool IsDaylightSavingTime { get; set; }

        [JsonProperty("dst_from")]
        public DateTimeOffset? DstFrom { get; set; }

        [JsonProperty("dst_offset")]
        public int DtsOffset { get; set; }

        [JsonProperty("dst_until")]
        public DateTimeOffset? DstUntil { get; set; }

        [JsonProperty("raw_offset")]
        public int RawOffset { get; set; }

        [JsonProperty("timezone")]
        public string? TimeZoneName { get; set; }

        [JsonProperty("unixtime")]
        public int UnixTime { get; set; }

        [JsonProperty("utc_datetime")]
        public DateTimeOffset UtcDateTime { get; set; }

    }
}
