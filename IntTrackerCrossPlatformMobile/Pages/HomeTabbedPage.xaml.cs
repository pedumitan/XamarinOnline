using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace IntTrackerCrossPlatformMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomeTabbedPage : TabbedPage
    {
        public static IList<IntTrackerGetList> TrackItems { get; set; }


        public HomeTabbedPage()
        {
            InitializeComponent();

            this.CurrentPage = this.setSchedulePage;
            //NavigationPage.SetHasNavigationBar(this, true);

            //Util.AlYamamabtn = AlYamamaRbt.IsChecked ? true : false;
            //Util.Sharmabtn = SharmaRbt.IsChecked ? true : false;
            //InternalRbt.IsChecked = Util.InternalMobilebtn ? true : false;
            //ContractorRbt.IsChecked = Util.ContractorMobilebtn ? true : false;

#pragma warning disable CS0618 // Type or member is obsolete
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom)
             .SetBarItemColor(Color.Black);
#pragma warning restore CS0618 // Type or member is obsolete
             //.SetBarSelectedItemColor(Color.Red);

            BarBackgroundColor = Color.DodgerBlue;

            //BarTextColor = Color.White;

            Load();
        }
        private void Load()
        {
            int totalRows = 0;
            //Util.AlYamamabtn = true;
            Util.connection = new SqlConnection(Util.connectionStringOnselection);
            Util.connection.Open();
            DataTable TrackDt = new DataTable();
            System.Data.DataTable TrackDtAllRows = new System.Data.DataTable();
            DataSet ds = null;
            SqlDataAdapter da = null;
            string dbo = Util.AlYamamabtn == true ? "[avitsql].[GetData]" : "[avitsql].[GetDataSharma]";


            using (SqlCommand cmd = new SqlCommand(dbo, Util.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TrackNoFrom", SqlDbType.VarChar).Value = null;// Util.TrackNoFrom;
                cmd.Parameters.Add("@TrackNoTo", SqlDbType.VarChar).Value = null;// Util.TrackNoTo;
                cmd.Parameters.Add("@TypeString", SqlDbType.VarChar).Value = Util.ContractorMobilebtn ? "Contractor" : "Internal";
                cmd.Parameters.Add("@StatusString", SqlDbType.VarChar).Value = "Open, In progress";//null;// Util.StatusString;
                cmd.Parameters.Add("@PriorityString", SqlDbType.VarChar).Value = null;// Util.PriorityString;
                cmd.Parameters.Add("@LocationString", SqlDbType.VarChar).Value = null;// Util.LocationString;
                cmd.Parameters.Add("@SubLocationString", SqlDbType.VarChar).Value = null;//Util.SubLocationString;
                cmd.Parameters.Add("@SpecLocationString", SqlDbType.VarChar).Value = null;//Util.SpecLocationString;
                cmd.Parameters.Add("@LocationDetailsString", SqlDbType.VarChar).Value = null;//Util.LocationDetailsString;
                cmd.Parameters.Add("@Today", SqlDbType.Int).Value = null;//Util.today;
                cmd.Parameters.Add("@CurWeek", SqlDbType.Int).Value = null;//Util.curWeek;
                cmd.Parameters.Add("@CurMonth", SqlDbType.Int).Value = null;//Util.curMonth;
                cmd.Parameters.Add("@DateReceivedIn", SqlDbType.VarChar).Value = null;//Util.DateCreatedIn;
                cmd.Parameters.Add("@DateReceivedOut", SqlDbType.VarChar).Value = null;//Util.DateCreatedOut;
                cmd.Parameters.Add("@ReportedByString", SqlDbType.VarChar).Value = null;//Util.ReportedByString;
                cmd.Parameters.Add("@DepartmentRefString", SqlDbType.VarChar).Value = null;//Util.DepartmentRefString;
                cmd.Parameters.Add("@AssignedToString", SqlDbType.VarChar).Value = null;//Util.AssignedToString;
                cmd.Parameters.Add("@EscalatedToString", SqlDbType.VarChar).Value = null;//Util.EscalatedToString;
                cmd.Parameters.Add("@DepartmentEsomString", SqlDbType.VarChar).Value = null;//Util.DepartmentEsomString;
                cmd.Parameters.Add("@ReportDateIn", SqlDbType.VarChar).Value = null;//Util.ReportDateIn;
                cmd.Parameters.Add("@ReportDateOut", SqlDbType.VarChar).Value = null;//Util.ReportDateOut;
                cmd.Parameters.Add("@PageNum", SqlDbType.Int).Value = 1; // pageNr; // Util.pageNum;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = 32; // Util.pageSize;
                cmd.Parameters.Add("@Order", SqlDbType.VarChar).Value = Util.Order;
                cmd.Parameters.Add("@Cols", SqlDbType.VarChar).Value = Util.Cols;

                cmd.ExecuteNonQuery();

                using (da = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    da.Fill(ds);

                }

                TrackDt = ds.Tables["Table"];
                totalRows = (from DataRow dr in TrackDt.Rows
                             select (int)dr["Total_Rows_Count"]).FirstOrDefault();

                //int r = 1;

                List<IntTrackerGetList> TrackList = new List<IntTrackerGetList>();

                foreach (DataRow row in TrackDt.Rows)
                {
                    IntTrackerGetList Track = new IntTrackerGetList();
                    Track.TrackId = (int)row["TrackId"];
                    Track.Type = row["Type"].ToString() != null ? row["Type"].ToString() : "";
                    Track.Status = row["Status"].ToString();
                    Track.Priority = row["Priority"].ToString();
                    Track.IssueDescription = row["IssueDescription"].ToString();
                    Track.Location = row["Location"].ToString();
                    Track.Sublocation = row["SublocationArea"].ToString() != null ? row["SublocationArea"].ToString() : "";
                    Track.Speclocation = row["SpecificLocation"].ToString() != null ? row["SpecificLocation"].ToString() : "";
                    Track.LocDetails = row["LocationDetails"].ToString() != null ? row["LocationDetails"].ToString() : "";
                    Track.DateCreated = row["DateCreated"].ToString() != null ? row["DateCreated"].ToString() : "";
                    Track.ReportedBy = row["ReportedBy"].ToString() != null ? row["ReportedBy"].ToString() : "";
                    Track.DepartmentEsom = row["DepartmentEsom"].ToString() != null ? row["DepartmentEsom"].ToString() : "";

                    TrackList.Add(Track);
                    //r++;

                }

                TrackItems = new ObservableCollection<IntTrackerGetList>(TrackList);

                IntTrackerDataGridView.ItemsSource = TrackItems; // IntTrackerViewPage.TrackItems;



            }
            Util.connection.Close();
            //LoadOrdersAsync();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {

            Navigation.InsertPageBefore(new FirstPage(), this); //(new MainPage(), this);
            await Navigation.PopAsync();
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (InternalRbt.IsChecked)
            {
                Util.InternalMobilebtn = true;
                Util.ContractorMobilebtn = false;
                this.Load();
                //LoadOrdersAsync(); 
                //Navigation.PushAsync(this);
            }
            if (ContractorRbt.IsChecked)
            {
                Util.InternalMobilebtn = false;
                Util.ContractorMobilebtn = true;
                this.Load();
                // Navigation.PushAsync(this);
            }
            if (AlYamamaRbt.IsChecked)
            {
                Util.AlYamamabtn = true;
                Util.Sharmabtn = false;
                this.Load();               
            }
            if (SharmaRbt.IsChecked)
            {
                Util.AlYamamabtn = false;
                Util.Sharmabtn = true;
                this.Load();
            }

        }

        //private void IntTrackerDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //Util.newItem = false;


        //    //DataRowView row = IntTrackerDataGridView.SelectedItem as DataRowView;

        //    //Util.trackId = Convert.ToInt32(row["TrackId"]);
        //    //Util.status = row["Status"].ToString();
        //}
        private async void OnCellDoubleClickClicked(object sender, EventArgs e)
        {
            Util.newItem = false;

            var itemsSelected = IntTrackerDataGridView.SelectedItem as IntTrackerGetList;

            Util.trackId = Convert.ToInt32(itemsSelected.TrackId);
            Util.type = itemsSelected.Type.ToString();
            Util.status = itemsSelected.Status.ToString();
            Util.priority = itemsSelected.Priority.ToString();
            Util.issueDesc = itemsSelected.IssueDescription.ToString();
            Util.location = itemsSelected.Location.ToString();
            Util.subLocation = itemsSelected.Sublocation.ToString();
            Util.specLocation = itemsSelected.Speclocation.ToString();
            Util.locationDetails = itemsSelected.LocDetails.ToString();
            Util.dateCreated = itemsSelected.DateCreated;
            Util.reportedBy = itemsSelected.ReportedBy.ToString();
            Util.departmentEsom = itemsSelected.DepartmentEsom.ToString();

            Navigation.InsertPageBefore(new IntTrackerEditForm(), this);
            await Navigation.PopAsync();
        }

        private void ToolbarItem_ChildAdded(object sender, ElementEventArgs e)
        {

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
            cmd.Parameters.AddWithValue("@CreatedBy", Util.userIdConnected);
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

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Util.connectionStringOnselection = Util.connectionStringLocal;
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}