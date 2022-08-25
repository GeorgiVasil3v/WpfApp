using Prism.Events;
using WpfApp.Base.Models;

namespace WpfApp.Core.Events
{
    internal class GetTimeZoneDataEvent : PubSubEvent<TimeZoneData?>
    {

    }
}
