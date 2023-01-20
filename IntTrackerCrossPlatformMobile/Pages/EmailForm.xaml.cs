using IntTrackerCrossPlatformMobile.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace IntTrackerCrossPlatformMobile
{
	public partial class EmailForm : ContentPage
	{		
		public EmailForm()
		{
			InitializeComponent();
			
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        async void btnSend_Click(object sender, System.EventArgs e)
        {
            List<string> toAddress = new List<string>();
            toAddress.Add(txtTo.Text);
            await SendEmail(txtSub.Text, txtContent.Text, toAddress);
        }
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try

            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device  
            }
            catch (Exception ex)
            {
                // Some other exception occurred  
            }
        }

    }
}
