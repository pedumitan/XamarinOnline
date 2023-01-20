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

namespace IntTrackerCrossPlatformMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntTrackerViewPage : ContentPage
    {
        public static IList<IntTrackerGetList> TrackItems { get; set; }
        public IntTrackerViewPage()
        {
            InitializeComponent();

            int totalRows = 0;
            Util.connection = new SqlConnection(Util.connectionStringOnselection);
            Util.connection.Open();
            DataTable TrackDt = new DataTable();
            System.Data.DataTable TrackDtAllRows = new System.Data.DataTable();
            DataSet ds = null;
            SqlDataAdapter da = null;
            string dbo = "[avitsql].[GetData]";


            using (SqlCommand cmd = new SqlCommand(dbo, Util.connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TrackNoFrom", SqlDbType.VarChar).Value = null;// Util.TrackNoFrom;
                cmd.Parameters.Add("@TrackNoTo", SqlDbType.VarChar).Value = null;// Util.TrackNoTo;
                cmd.Parameters.Add("@TypeString", SqlDbType.VarChar).Value = null;// Util.TypeString;
                cmd.Parameters.Add("@StatusString", SqlDbType.VarChar).Value = "Open,In progress";//null;// Util.StatusString;
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

                IntTrackerDataGridView.ItemsSource = IntTrackerViewPage.TrackItems;



            }
            Util.connection.Close();
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {

            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync();
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
    }
}