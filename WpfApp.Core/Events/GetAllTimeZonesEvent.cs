using Prism.Events;

namespace WpfApp.Core.Events
{
    internal class GetAllTimeZonesEvent : PubSubEvent<string[]>
    {
    }
}
