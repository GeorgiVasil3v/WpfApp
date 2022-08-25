using WpfApp.Base.Models;

namespace WpfApp.Base.Interfaces
{
    internal interface ITimeZoneService
    {
        Task<string[]> GetAllTimeZones();
        Task<TimeZoneData?> GetTimeZoneData(string timeZone);
    }
}
