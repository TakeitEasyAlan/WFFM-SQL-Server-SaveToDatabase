/*************************************************************************
The MIT License (MIT)

Copyright (c) 2014 Bo Breiting

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
**************************************************************************/
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace WFFM.SQLServer.SaveToDatabase.Infrastructure.Commands
{
    internal class ExportFormDataCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            Assert.IsNotNull(context.Items, "context items are null");
            Assert.IsTrue(context.Items.Length > 0, "context items length is 0");

            Item contextItem = context.Items[0];
            Assert.IsNotNull(contextItem, "First context item is null");
            OpenNewWindow(contextItem.ID, contextItem.Name);
        }

        private void OpenNewWindow(ID id, string name)
        {
            Assert.ArgumentNotNull(id, "id");
            UrlString url = new UrlString(Constants.Url.ExportFromDataPage);
            url.Append(Constants.QueryString.Name.ItemId, HttpContext.Current.Server.UrlEncode(id.ToString()));
            url.Append(Constants.QueryString.Name.ItemName, HttpContext.Current.Server.UrlEncode(name));
            SheerResponse.Eval(string.Format("window.open('{0}');", url));
        }
    }
} 
