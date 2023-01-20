using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace IntTrackerCrossPlatformMobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged  //: Base.PageModelBase
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string _username;
        public string Username
        {
            get => _username;
            //set => SetProperty(ref _username, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            //set => SetProperty(ref _password, value);
        }



       
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            //if (email != "macoratti@yahoo.com" || password != "secret")
            //{
            //    DisplayInvalidLoginPrompt();
            //}
        }
    }
}
