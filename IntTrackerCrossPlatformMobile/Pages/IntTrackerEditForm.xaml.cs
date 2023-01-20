using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntTrackerCrossPlatformMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class IntTrackerEditForm : ContentPage
    {

        // Dictionary to get Color from color name.
        //Dictionary<string, Color> Speclocation = new Dictionary<string, Color>
        //{
        //    { "Aqua", Color.Aqua }, { "Black", Color.Black },
        //    { "Blue", Color.Blue }, { "Fuchsia", Color.Fuchsia },
        //    { "Gray", Color.Gray }, { "Green", Color.Green },
        //    { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
        //    { "Navy", Color.Navy }, { "Olive", Color.Olive },
        //    { "Purple", Color.Purple }, { "Red", Color.Red },
        //    { "Silver", Color.Silver }, { "Teal", Color.Teal },
        //    { "White", Color.White }, { "Yellow", Color.Yellow }
        //};

        //private ToolbarItem _BackBtnToolBarItem;

        public IntTrackerEditForm()
        {
            InitializeComponent();

            // NavigationPage.SetHasNavigationBar(this, true); SupportActionBar.SetHomeButtonEnabled (true);
            //NavigationPage.SetHasBackButton(this, true);

            //public override bool OnOptionsItemSelected(IMenuItem item)
            //{
            //    if (item.TitleFormatted == null) this.OnBackPressed();
            //    return base.OnOptionsItemSelected(item);
            //}

            //ToolbarItems.Add(new ToolbarItem("Back", "",()  => //OnBackButtonPressed()
            //{

            //}));
              

            // _BackBtnToolBarItem = new ToolbarItem()
            //  {IconImageSource = "return", Order = ToolbarItemOrder.Primary, Priority = 0, Text = "Back" };
            //ToolbarItems.Add(_BackBtnToolBarItem);
            //_BackBtnToolBarItem.Clicked += OnBackButtonClicked;


            //Content = new StackLayout
            //{
            //    //Children = {
            //    //new Label {
            //    //    Text = "Todo list data goes here",
            //    //    HorizontalOptions = LayoutOptions.Center,
            //    //    VerticalOptions = LayoutOptions.CenterAndExpand
            //    //}
            ////}
            //};

            if (Util.userConnected.ToLower().Equals("Guest".ToLower()))
            {
                SaveButton.IsEnabled = false;
            }

            if (Util.InternalMobilebtn)
            {
                DepartmentContractorLbl.IsVisible = false;
                DepartmentContractor.IsVisible = false;
                //IssuedToLbl.IsVisible = false;
                //IssuedTo.IsVisible = false;
            }
            if (Util.ContractorMobilebtn)                
            {
                IssuedToLbl.IsVisible = false;
                IssuedTo.IsVisible = false;
                //DepartmentContractorLbl.IsVisible = false;
                //DepartmentContractor.IsVisible = false;
            }
            if (Util.newItem == true)
            {
                TrackIdlbl.IsVisible = false;
                TrackIdTBox.IsVisible = false;
            }
            else
            {
                TrackIdlbl.IsVisible = true;
                TrackIdTBox.IsVisible = true;

                TrackIdTBox.Text = Util.trackId.ToString();
                Status.SelectedItem = Util.status;
                Priority.SelectedItem = Util.priority;
                TaskDescription.Text = Util.issueDesc;
                Location.SelectedItem = Util.location;
                Sublocation.Text = Util.subLocation;
                Speclocation.Text = Util.specLocation;
                LocDetails.Text = Util.locationDetails;
                IssueDate.Date = DateTime.ParseExact(Convert.ToDateTime(Util.dateCreated).ToString("MM/dd/yyyy HH:mm").Replace("-", "/").Replace(".", "/"), "MM/dd/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);//(Util.dateCreated);
                ReportedBy.Text = Util.reportedBy.ToString();
                DepartmentContractor.SelectedItem = Util.departmentEsom.ToString();
            }
        }
        async void OnSaveFormButtonClicked(object sender, EventArgs e)
        {
            Util.connection = new SqlConnection(Util.connectionStringOnselection);
            Util.connection.Open();


            String st = "";
            if (Util.newItem)
                st = "INSERT INTO [dbo].[TechIntTracker] (DateCreated,Type,CreatedBy,ReportedBy,Position, DepartmentInternal,AssignedTo,LastUpdate,HostMachine,HostMaccAdress,IPAdress,Testing,Priority,Status,DepartmentEsom,Location,SublocationArea,SpecificLocation,LocationDetails,IssueDescription) values (@DateCreated,@Type,@CreatedBy,@ReportedBy,@Position,@DepartmentInternal,@AssignedTo,@LastUpdate,@HostMachine,@HostMaccAdress,@IPAdress,@Testing,@Priority,@Status,@DepartmentEsom,@Location,@SublocationArea,@SpecificLocation,@LocationDetails,@IssueDescription)";
            else
                st = "UPDATE [dbo].[TechIntTracker] SET " +
                    "DateCreated =  @DateCreated," +
                    "Type = @Type,CreatedBy = @CreatedBy,ReportedBy = @ReportedBy," +
                    "Position = @Position, " +
                    "DepartmentInternal = @DepartmentInternal," +
                    "AssignedTo = @AssignedTo,LastUpdate = @LastUpdate,HostMachine = @HostMachine,HostMaccAdress = @HostMaccAdress," +
                    "IPAdress = @IPAdress," +
                    "Testing = @Testing," +
                    "Priority = @Priority,Status = @Status,DepartmentEsom = @DepartmentEsom," +
                    "Location = @Location,SublocationArea = @SublocationArea,SpecificLocation = @SpecificLocation,LocationDetails = @LocationDetails,IssueDescription = @IssueDescription " +
                    "WHERE TrackId = " + Util.trackId;
                    
            SqlCommand cmd = new SqlCommand(st, Util.connection);

            cmd.Parameters.AddWithValue("@DateCreated", DateTime.ParseExact(IssueDate.Date.ToString("MM/dd/yyyy hh:mm"), "MM/dd/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@Type", Util.ContractorMobilebtn ? "Contractor".ToString() : "Internal".ToString());
            cmd.Parameters.AddWithValue("@CreatedBy",Util.userIdConnected);
            cmd.Parameters.AddWithValue("@ReportedBy", Util.fullNameConnected.ToString());
            cmd.Parameters.AddWithValue("@Position", Util.positionUserConnected.ToString());
            cmd.Parameters.AddWithValue("@DepartmentInternal", Util.departmentConnected.ToString());
            cmd.Parameters.AddWithValue("@LastUpdate", DateTime.ParseExact(IssueDate.Date.ToString("MM/dd/yyyy hh:mm"), "MM/dd/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@HostMachine", Util.userMachine.ToString());
            cmd.Parameters.AddWithValue("@HostMaccAdress", Util.userMachineMacc.ToString());
            cmd.Parameters.AddWithValue("@IPAdress", Util.userIp.ToString());
            cmd.Parameters.AddWithValue("@Testing", "0".ToString());
            cmd.Parameters.AddWithValue("@Priority", Priority.SelectedItem != null ? Priority.SelectedItem.ToString() : "LOW");
            cmd.Parameters.AddWithValue("@Status", Status.SelectedItem != null ? Status.SelectedItem.ToString() : "Open");
            cmd.Parameters.AddWithValue("@DepartmentEsom", DepartmentContractor.SelectedItem != null ? DepartmentContractor.SelectedItem.ToString() : "");
            cmd.Parameters.AddWithValue("@AssignedTo", IssuedTo.SelectedItem != null ? IssuedTo.SelectedItem.ToString() : "");
            cmd.Parameters.AddWithValue("@Location", Location.SelectedItem != null ? Location.SelectedItem.ToString() : "MV");
            cmd.Parameters.AddWithValue("@SublocationArea", Sublocation.Text != null ? Sublocation.Text : "");
            cmd.Parameters.AddWithValue("@SpecificLocation", Speclocation.Text != null ? Speclocation.Text : "");
            cmd.Parameters.AddWithValue("@LocationDetails", LocDetails.Text != null ? LocDetails.Text : "");
            cmd.Parameters.AddWithValue("@IssueDescription", TaskDescription.Text != null ? TaskDescription.Text : "");

            cmd.ExecuteNonQuery();
            Util.connection.Close();

            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
        }

      
        async void OnCloseFormButtonClicked(object sender, EventArgs e)
        {           
            Util.InternalMobilebtn = false;
            Util.ContractorMobilebtn = false;

            if (Util.newItem)
                Navigation.InsertPageBefore(new MainPage(), this);
            else
                Navigation.InsertPageBefore(new IntTrackerViewPage(), this);
            await Navigation.PopAsync();
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            if(Util.newItem)
            Navigation.InsertPageBefore(new MainPage(), this);
            else
                Navigation.InsertPageBefore(new IntTrackerViewPage(), this);
            await Navigation.PopAsync();
        }
    }
}