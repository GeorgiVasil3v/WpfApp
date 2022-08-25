namespace WpfApp.Core.Services
{
    internal class ApiUrls
    {
        private const string BaseUtl = "http://worldtimeapi.org/api/";
        public static readonly string AllTimeZones = string.Format($"{BaseUtl}timezone");
        
        public static string GetTimeZoneUrl(string timeZone)
        {
            return string.Format($"{BaseUtl}timezone/{timeZone}");
        }
    }
}
