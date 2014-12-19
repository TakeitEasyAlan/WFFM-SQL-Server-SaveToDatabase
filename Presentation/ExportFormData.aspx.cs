using System;
using System.Web;
using Sitecore.Data;
using WFFM.SQLServer.SaveToDatabase.Application;

namespace WFFM.SQLServer.SaveToDatabase.Presentation
{
    public partial class ExportFormData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //you have to be logged in, and have the form id as a query string
            if (!Sitecore.Context.IsLoggedIn)
            {
                NotLoggedLoggedInPlaceHolder.Visible = true;
                return;
            }

            string itemName = HttpContext.Current.Server.UrlDecode(Request.QueryString[Constants.QueryString.Name.ItemName]);
            if (string.IsNullOrEmpty(itemName))
            {
                NotLoggedLoggedInPlaceHolder.Visible = true;
                return;
            }

            string id = HttpContext.Current.Server.UrlDecode(Request.QueryString[Constants.QueryString.Name.ItemId]);
            if (string.IsNullOrEmpty(id))
            {
                NotLoggedLoggedInPlaceHolder.Visible = true;
                return;
            }

            _exportToCsvService.ExportToCsv(Response, new ID(id), itemName);
        }

        readonly ExportToCsvService _exportToCsvService = new ExportToCsvService();
    }
}