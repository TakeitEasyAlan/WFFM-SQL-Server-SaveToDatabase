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
*************************************************************************/

using System.Collections.Generic;
using System.Linq;
using PT.Framework.Form;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Data.Submit;
using Sitecore.Forms.Data;
using WFFM.SQLServer.SaveToDatabase.Infrastructure.Data;

namespace WFFM.SQLServer.SaveToDatabase.Model
{
  public class FormReposiotry
  {
    public void Insert(ID formId, AdaptedResultList fields, ID sessionID, string data)
    {
      Assert.ArgumentNotNull(formId, "formId");
      Assert.ArgumentNotNull(fields, "fields");

      Infrastructure.Data.Form form = _formFactory.Create(formId, fields, sessionID, data);
      using (WebFormForMarketersDataContext webFormForMarketersDataContext = new WebFormForMarketersDataContext(ConnectionString))
      {
        webFormForMarketersDataContext.Forms.InsertOnSubmit(form);
        webFormForMarketersDataContext.SubmitChanges();
      }

    }

    public IEnumerable<IForm> Get(ID formId)
    {
 
      List<IForm> forms = new List<IForm>();

      using (WebFormForMarketersDataContext webFormForMarketersDataContext = new WebFormForMarketersDataContext(ConnectionString))
      {
        IQueryable<Infrastructure.Data.Form> dataForms = webFormForMarketersDataContext.Forms.Where(f => f.FormItemId == formId.Guid).OrderBy(f => f.Timestamp);
        foreach (Infrastructure.Data.Form dataForm in dataForms)
        {
          Form form = _formFactory.Create(dataForm);
          if (form != null)
            forms.Add(form);
        }
      }
      return forms;
    }

    private string ConnectionString
    {
      get { return Settings.GetConnectionString(Settings.GetSetting(Constants.Settings.Name.Connection)); }
    }
    private readonly FormFactory _formFactory = new FormFactory();
  }
}