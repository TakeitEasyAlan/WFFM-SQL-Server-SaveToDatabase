using System;
using System.Web;
using Sitecore.Data;
using WFFM.SQLServer.SaveToDatabase.Application;
using WFFM.SQLServer.SaveToDatabase.Model;

namespace WFFM.SQLServer.SaveToDatabase.Presentation
{
    public partial class ExportFormData : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //you have to be logged in, and have the form id as a query string
            if (!Sitecore.Context.IsLoggedIn ||
                string.IsNullOrEmpty(this.ItemID) ||
                string.IsNullOrEmpty(this.ItemName))
            {
                NotLoggedLoggedInPlaceHolder.Visible = true;
                return;
            }

            // If export options enabled show the date range UI options, otherwise just export all data immediately
            var exportOptions = Sitecore.Configuration.Settings.GetBoolSetting(Constants.Settings.Name.ExportOptions, false);
            if (exportOptions)
            {
                WithDateRange.Visible = true;

                if (IsPostBack) 
                    return;
                txtFrom.Text = _formRepository.OldestFormSubmission(new ID(this.ItemID)).ToString(Constants.DateTime.DateFormat);
                txtTo.Text = DateTime.Now.ToString(Constants.DateTime.DateFormat);
            }
            else
            {
                _exportToCsvService.ExportToCsv(Response, new ID(this.ItemID), this.ItemName);
            }
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            DateTime from;
            if (!DateTime.TryParse(txtFrom.Text, out from))
            {
                warnFrom.Text = Constants.Texts.Warning.IncorrectDateFromat;
                return;
            }

            DateTime to;
            if (!DateTime.TryParse(txtTo.Text, out to))
            {
                warnTo.Text = Constants.Texts.Warning.IncorrectDateFromat; ;
                return;
            }

            // If passed in "3/17/2015"; covert it to "3/17/2015 23:59:59"
            if (to.Hour == 0 && to.Minute == 0 && to.Second == 0)
                to = to.Add(new TimeSpan(23, 59, 59));

            _exportToCsvService.ExportToCsv(Response, new ID(this.ItemID), this.ItemName, from, to);
        }


        private string _itemName = null;
        private string ItemName
        {
            get
            {
                if (_itemName == null)
                    _itemName = HttpContext.Current.Server.UrlDecode(Request.QueryString[Constants.QueryString.Name.ItemName]) ?? string.Empty;
                return _itemName;
            }
        }


        private string _itemID = null;
        private string ItemID
        {
            get
            {
                if (_itemID == null)
                    _itemID = HttpContext.Current.Server.UrlDecode(Request.QueryString[Constants.QueryString.Name.ItemId]) ?? string.Empty;
                return _itemID;
            }
        }


        readonly ExportToCsvService _exportToCsvService = new ExportToCsvService();
        readonly FormRepository _formRepository = new FormRepository();
    }
}