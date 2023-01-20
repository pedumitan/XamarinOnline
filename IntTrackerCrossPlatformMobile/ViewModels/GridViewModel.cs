using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntTrackerCrossPlatformMobile.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<IntTrackerGetList> _trackItems;
        private IntTrackerGetList _selectedIntTrackerGetList;
        private bool _isRefreshing;

        public List<IntTrackerGetList> IntTrackerGetLists
        {
            get
            {
                return _trackItems;
            }
            set
            {
                _trackItems = value;
                OnPropertyChanged(nameof(IntTrackerGetLists));
            }
        }
        public IntTrackerGetList SelectedIntTrackerGetList
        {
            get
            {
                return _selectedIntTrackerGetList;
            }
            set
            {
                _selectedIntTrackerGetList = value;
                OnPropertyChanged(nameof(SelectedIntTrackerGetList));
            }
        }
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        //public ICommand RefreshCommand { get; set; }
        //public MainPageViewModel()
        //{
        //    IntTrackerGetLists = DummyProfessionalData.GetProfessionals();
        //    RefreshCommand = new Command(CmdRefresh);
        //}

        //private async void CmdRefresh()
        //{
        //    //IsRefreshing = true;
        //    //await IntTrackerViewPage.Delay(3000);
        //    //IsRefreshing = false;
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


        //    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        //    {
        //        if (!(object.Equals(field, newValue)))
        //        {
        //            field = (newValue);
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //            return true;
        //        }

        //        return false;
        //    }

        //    private System.Collections.IEnumerable professionals;

        //    public System.Collections.IEnumerable Professionals { get => professionals; set => SetProperty(ref professionals, value); }

        //    private object selectedProfesstional;

        //    public object SelectedProfesstional { get => selectedProfesstional; set => SetProperty(ref selectedProfesstional, value); }
    }
}