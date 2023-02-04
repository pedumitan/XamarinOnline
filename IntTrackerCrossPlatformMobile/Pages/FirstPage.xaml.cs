using IntTrackerCrossPlatformMobile.Pages;
using System;
using Xamarin.Forms;

namespace IntTrackerCrossPlatformMobile
{
	public partial class FirstPage : ContentPage
	{		
		public FirstPage()
		{
			InitializeComponent();

			this.Title = "Welcome, " + Util.userConnected + "!";

			//ContractorRbt.IsEnabled = Util.departmentConnected.ToLower() == "technical" ? true : false;
			//ContractorRbt.IsChecked = Util.departmentConnected.ToLower() == "technical" ? true : false;			
			//Util.AlYamamabtn = false;
			//Util.Sharmabtn = false;
			Util.AlYamamabtn = AlYamamaRbt.IsChecked ? true : false;
			Util.Sharmabtn = SharmaRbt.IsChecked ? true : false;
			//Util.InternalMobilebtn = InternalRbt.IsChecked ? true : false;
			//Util.ContractorMobilebtn = ContractorRbt.IsChecked ? true : false;
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			//if (InternalRbt.IsChecked)
			//{
			//	Util.InternalMobilebtn = true;
			//	Util.ContractorMobilebtn = false;
			//}
			//if(ContractorRbt.IsChecked)
   //         {
			//	Util.InternalMobilebtn = false; 
			//	Util.ContractorMobilebtn = true;
			//}
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			App.IsUserLoggedIn = false;
			Util.connectionStringOnselection = Util.connectionStringLocal;
			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}

		//async void OnIntTrackerEditFormButtonClicked(object sender, EventArgs e)
		//{
		//	//Util.newItem = true;
		//	//Navigation.InsertPageBefore(new IntTrackerEditForm(), this);
		//	//await Navigation.PopAsync();
		//}		
		async void OnIntTrackerViewFormButtonClicked(object sender, EventArgs e)
		{
            Navigation.InsertPageBefore(new HomeTabbedPage(), this); //(new IntTrackerViewPage(), this);
            await Navigation.PopAsync();
        }
		
		async void OnEmailButtonClicked(object sender, EventArgs e)
		{
			Navigation.InsertPageBefore(new EmailForm(), this);
			await Navigation.PopAsync();
		}
	}
}
