using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Xamarin.Forms.Platform.Android;
using IntTrackerCrossPlatformMobile.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.ComponentModel;
using Xamarin.Forms.PlatformConfiguration;
using IntTrackerCrossPlatformMobile.Pages;
using FormsElement = Xamarin.Forms.Entry;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
//using System.ComponentModel.DescriptionAttribute;

namespace IntTrackerCrossPlatformMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]    

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            usernameEntry.On<Android>().SetAutofillHint(AutofillHintType.Username);
            passwordEntry.On<Android>().SetAutofillHint(AutofillHintType.Password);

            var mac = NetworkInterface.GetAllNetworkInterfaces();
            var macAddress = String.Join(":", mac[0].GetPhysicalAddress()
                                    .GetAddressBytes()
                                    .Select(b => b.ToString("X2"))
                                    .ToArray());
            Util.userMachineMacc = macAddress;

            messageLabel.Text = "" + Environment.NewLine + "";

        }
        
        async void OnPutIn(object sender, EventArgs e)
        {
            //if (await CrossFingerprint.Current.IsAvailableAsync(true))
            //{
            //    var result = await CrossFingerprint.Current.AuthenticateAsync(
            //       new AuthenticationRequestConfiguration("Login", "Access your account"));
            //    if (result.Authenticated)
            //    {
            //        await DisplayAlert("Success", "Authenticated", "OK");
            //    }
            //    else
            //    {
            //        await DisplayAlert("Failure", "Not Authenticated", "OK");
            //    }
            //}
            //else
            //{
            //    await DisplayAlert("Failure", "Biometrics not available", "OK");
            //}
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {         

            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            if (usernameEntry.Text == null)
            {
                usernameEntry.Text = "";
                messageLabel.Text = "User field cannot be empty. Please try again! "+ Environment.NewLine + "Login failed!";
            }
            if (passwordEntry.Text == null)
            {
                passwordEntry.Text = "";
            }
            else
            {

                var isValid = AreCredentialsCorrect(user);
                if (isValid)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = messageLabel.Text + Environment.NewLine + "Login failed!";
                }
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            //dBoSwitch.Toggled += HandleSwitchToggledByUser;

            Util.connectionStringOnselection = dBoSwitch.IsToggled ? Util.connectionString : Util.connectionStringLocal;
        }      

        private void selectUser(string userCheck)
        {
            //int totalRows = 0;
            //int[] users = new int[10]; 

            string connectionString;
            SqlConnection connection;
            //connectionString = @"Data Source=188.213.132.104,1533; User ID=avitsql;Password=Mranderson1!;";// Trusted_Connection = true
            connectionString = Util.connectionStringOnselection;
                //@"Data Source=188.213.133.2,1533; Database=florent_hillrobinsontech; User ID=florent_avitsql;Password=BlackMatza777!!!???";
            connection = new SqlConnection(connectionString);
            connection.Open();
            //Util.connection.Open();
            DataTable TrackDt = new DataTable();
            System.Data.DataTable TrackDtAllRows = new System.Data.DataTable();
            DataSet ds = null;
            SqlDataAdapter da = null;
            string dbo = "[dbo].[GetUserInfo]";


            using (SqlCommand cmd = new SqlCommand(dbo, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userCheck;              

                cmd.ExecuteNonQuery();

                using (da = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    da.Fill(ds);
                }

                TrackDt = ds.Tables["Table"];

                if (TrackDt.Rows.Count > 0)
                {
                    Util.userConnected = TrackDt.Rows[0]["userName"].ToString();
                    Util.fullNameConnected = TrackDt.Rows[0]["Name"].ToString();
                    Util.departmentConnected = TrackDt.Rows[0]["Department"].ToString();
                    // Util.userTypeConnected = userItem.userType;
                    Util.userProject = TrackDt.Rows[0]["DefaultProject"].ToString();
                    Util.userIdConnected = (int)TrackDt.Rows[0]["Id"];
                    Util.userEmailConnected = TrackDt.Rows[0]["Email"].ToString();
                    Util.active = (int)TrackDt.Rows[0]["Active"];
                    Util.positionUserConnected = TrackDt.Rows[0]["Position"].ToString();
                    Util.codedPassDatabase = TrackDt.Rows[0]["Password"].ToString();
                }
            }
            connection.Close();
        }

        bool AreCredentialsCorrect(User user)
        {
            //Util.userProject = user.DefaultProject.ToString();
            Util.passw = user.Password;
            string codedPass = null;
            selectUser(user.Username.ToString());
            //string checkedUser = user.Username.ToString();

            if (Util.userConnected == "")
            {
                messageLabel.Text = "Wrong user! Please try again!";
                usernameEntry.Text = string.Empty;
                return false;
            }

            else
            {

                string userReturned = Util.userConnected;
           
                Cripted.cripted();
                codedPass = Util.passCript;

                if (!String.Equals(codedPass, Util.codedPassDatabase))
                {
                    messageLabel.Text = "Wrong password! Please try again!";
                    passwordEntry.Text = string.Empty;
                    return false;
                }

                return user.Username.Trim().ToLower() == userReturned.Trim().ToLower() && Util.codedPassDatabase == codedPass;

            }
        }
    }
}
