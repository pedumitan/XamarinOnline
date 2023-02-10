using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace IntTrackerCrossPlatformMobile
{
    class Util
    {
        public static string currentSession = "";

        public static string fullNameConnected = "";
        public static string departmentConnected = "";
        public static string userTypeConnected = "";
        public static string userEmailConnected = "";
        public static string oldPassw = "";
        public static string passw = "";
        public static string userConnected = "";
        public static    int userIdConnected = 0;
        public static string positionUserConnected = "";
        public static string passCript = "";
        public static string codedPassDatabase = "";
        public static Boolean persAccount = false;


        public static int trackId = 0;
        public static string type = "";
        public static string contractor = "";
        public static string status = "";
        public static string priority = "";
        public static string duringAfterVisit = "";
        public static string issueDesc = "";
        public static string location = "";
        public static string subLocation = "";
        public static string specLocation = "";
        public static string locationDetails = "";

        //public static string dateReceived = "";
        public static string dateCreated = "";
        public static string dueDate = "";
        public static string daysOpen = "";
        public static string reportedBy = "";
        public static string Owner = "";
        public static string assignedTo = "";

        public static string departmentRef = "";

        public static string escalatedTo = "";
        public static string departmentEsom = "";
        public static string departmentInternal = "";
        public static Boolean escalation = false;
        public static Boolean ContrToInternal = false;
        public static string InternalRef = "";
        public static int ContractorTrackIdInternalRef = 0;
        public static string esomRef = "";
        public static string reportDate = "";
        public static string closingComment = "";
        public static string followUp = "";
        public static string ActionPlan = "";
        public static string MaterialsNeeded = "";


        public static string closedBy = "";
        public static string inspDate = "";
        public static string ClosingDate = "";
        public static string RecordType = "";
        public static string EscalComm = "";
        public static string EscalCompBy = "";
        public static string EscalStartDate = "";
        public static string EscalComplDate = "";
        public static string Manager = "";
        public static string TechnicianAssigned = "";

        public static string HLRFReport = "";
        public static string ContractorFReport = "";

        public static bool newItem = false;

        //filters
        public static bool FilterUsed = false;
        public static List<string> statusFilterLists = new List<string>();
        public static string TypeString = "";
        public static string StatusString = "";
        public static string PriorityString = "";
        public static string LocationString = "";
        public static string DateCreatedIn = "";
        public static string DateCreatedOut = "";
        public static string ReportedByString = "";
        public static string DepartmentRefString = "";
        public static string AssignedToString = "";
        public static string EscalatedToString = "";
        public static string DepartmentEsomString = "";
        public static string ReportDateIn = "";
        public static string ReportDateOut = "";

        public static int today = 0;
        public static int curWeek = 0;
        public static int curMonth = 0;
        public static int wholeList = 1;


        public static int CloseDropdownNr = 1;

        public static bool SaveError = false;

        //users
        public static bool newUser = false;
        public static int userId = 0;
        public static string userType = "";
        public static string userProject = "";
        public static string department = "";
        public static string position = "";
        public static string user = "";
        public static string pass = "";
        public static string name = "";
        public static string Email = "";
        public static int active = 0;
        public static string userIp = Dns.GetHostAddresses(Environment.MachineName)[0].ToString();
        public static string userMachine = Environment.MachineName;
        public static string userMachineMacc = "";
        public static string windowsAccount = "";

        //flags
        public static bool filterSel = false;
        public static bool filterAllSel = false;

        //locations
        public static bool Sharmabtn = false;
        public static bool AlYamamabtn = true;

        public static bool InternalMobilebtn = false;
        public static bool ContractorMobilebtn = true;

        //page
        public static int pageNum = 0;
        public static int pageSize = 32;
        public static string Order = "TrackId DESC";
        public static string Cols = " * ";

        public static int pageNumCont = 0;
        public static int pageSizeCont = 32;
        public static string OrderCont = "Id DESC";
        public static string ColsCont = " * ";

        //export location
        public static string exportLocation = "";

        //date format
        //public static IFormatProvider culture = new CultureInfo("en-US", true);
        public static DateTime NADate = DateTime.ParseExact(Convert.ToDateTime("01/01/1900 00:00:00").ToString("MM/dd/yyyy HH:mm").Replace("-", "/").Replace(".", "/"), "MM/dd/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

        //permission
        public static int permissionId = 0;
        public static string permissionName = "";
        public static string permissionDescription = "";
        public static bool newPermission = false;

        //dbo connection        
        public static string connectionStringLocal = @"Data Source=10.19.107.154, 1433; Database=florent_hillrobinsontech_local; User ID=sa;Password=Mranderson1!";
        public static string connectionString = @"Data Source=188.213.133.2,1533; Database=florent_hillrobinsontech; User ID=florent_avitsql;Password=BlackMatza777!!!???";
        public static string connectionStringOnselection = connectionStringLocal;
        //@"Data Source=172.20.7.76, 1434; Database=florent_hillrobinsontech_local; User ID=sa;Password=Mranderson1!";
        //@"Data Source=188.213.133.2,1533; Database=florent_hillrobinsontech; User ID=florent_avitsql;Password=BlackMatza777!!!???";
        //Current Cloud server @"Data Source=188.213.133.2,1533; Database=florent_hillrobinsontech; User ID=florent_avitsql;Password=BlackMatza777!!!???";
        //AY Current server @"Data Source=10.19.107.154,1433; User ID=sa;Password=Mranderson1!";
        //@"Data Source=FLORENTINHP\SQLEXPRESS2019; Database=florent_hillrobinsontech_local; User ID=sa;Password=Mranderson1!";
        //@"Data Source=188.213.133.2,1533; Database=florent_hillrobinsontech; User ID=florent_avitsql;Password=BlackMatza777!!!???";
        //Old Cloud Server@"Data Source=188.213.132.104,1533; User ID=avitsql;Password=Mranderson1!";
        public static SqlConnection connection;//= new SqlConnection(connectionStringOnselection);
    }
}
