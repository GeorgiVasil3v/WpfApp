using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp.Base.Interfaces;
using WpfApp.Base.Models;
using WpfApp.Core.Events;

namespace WpfApp.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        #region Fields

        readonly IEventAggregator _ea;
        readonly ITimeZoneService _tzService;

        #endregion

        #region Properties

        private string _title = "Wpf Sample";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isLoadingData = false;

        public bool IsLoadingData
        {
            get { return _isLoadingData; }
            set { SetProperty(ref _isLoadingData, value); }
        }

        public ObservableCollection<string> TimeZones { get; private set; } = new ObservableCollection<string>();

        private string _selectedTimeZone = string.Empty;
        public string SelectedTimeZone
        {
            get { return _selectedTimeZone; }
            set { SetProperty(ref _selectedTimeZone, value, LoadTimeZoneData); }
        }

        private TimeZoneData? _receivedTimeZoneData;
        public TimeZoneData? ReceivedTimeZoneData
        {
            get { return _receivedTimeZoneData; }
            set { SetProperty(ref _receivedTimeZoneData, value); }
        }

        #endregion

        #region Commands

        public DelegateCommand GetAllTimeZonesCommand { get; private set; }

        #endregion

        #region Constructors

        public MainWindowViewModel(IEventAggregator ea, ITimeZoneService tzService)
        {
            _ea = ea;
            _tzService = tzService;
            GetAllTimeZonesCommand = new DelegateCommand(LoadAllTimeZones);

            _ea.GetEvent<GetAllTimeZonesEvent>().Subscribe(TimeZonesReceived);
            _ea.GetEvent<GetTimeZoneDataEvent>().Subscribe(TimeZoneDataReceived);
        }

        #endregion

        #region EA Subscribers

        private void TimeZonesReceived(string[] timeZones)
        {
            IsLoadingData = false;

            if (timeZones == null || timeZones.Length == 0)
            {
                MessageBox.Show("No time zones found!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (string tz in timeZones)
                TimeZones.Add(tz);

            if (TimeZones.Count > 0)
                SelectedTimeZone = TimeZones[0];
;
        }

        private void TimeZoneDataReceived(TimeZoneData? tzData)
        {
            IsLoadingData = false;

            if (tzData == null)
            {
                MessageBox.Show("Time zone not found!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ReceivedTimeZoneData = tzData;
        }

        #endregion

        #region EA Publishers

        private async void LoadAllTimeZones()
        {
            IsLoadingData = true;

            SelectedTimeZone = string.Empty;
            ReceivedTimeZoneData = null;
            TimeZones.Clear();

            var result = await _tzService.GetAllTimeZones();
            _ea.GetEvent<GetAllTimeZonesEvent>().Publish(result);
        }

        private async void LoadTimeZoneData()
        {
            if (string.IsNullOrEmpty(SelectedTimeZone))
                return;

            IsLoadingData = true;

            var result = await _tzService.GetTimeZoneData(SelectedTimeZone);
            _ea.GetEvent<GetTimeZoneDataEvent>().Publish(result);
        }

        #endregion
    }
}
