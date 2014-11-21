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


using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Data.Submit;
using Sitecore.Form.Core.Controls.Data;

namespace WFFM.SQLServer.SaveToDatabase.Model
{
  internal class FormFactory
  {
    internal Infrastructure.Data.Form Create(ID formId, AdaptedResultList fields, ID sessionID, string data)
    {
      Assert.ArgumentNotNull(formId, "formId");
      Assert.ArgumentNotNull(fields, "fields");

      Infrastructure.Data.Form form = new Infrastructure.Data.Form
      {
        Id = Guid.NewGuid(),
        FormItemId = formId.Guid,
        SessionId = sessionID.ToGuid(),
        Data = data,
        StorageName = string.Empty,
        Timestamp = DateTime.Now
      };

      foreach (AdaptedControlResult adaptedControlResult in fields)
      {
        Infrastructure.Data.Field field = new Infrastructure.Data.Field
        {
          Id = Guid.NewGuid(),
          FieldName = adaptedControlResult.FieldName,
          Data = adaptedControlResult.Parameters ?? string.Empty,
          Value = ((adaptedControlResult.Parameters != null) && adaptedControlResult.Parameters.StartsWith("secure:"))
            ? Regex.Replace(adaptedControlResult.Parameters, "<schidden>.*</schidden>", "<schidden></schidden>")
            : adaptedControlResult.Value
        };

        form.Fields.Add(field);
      }

      return form;
    }

    internal Form Create(Infrastructure.Data.Form dataForm)
    {
      Form form = new Form(dataForm.Timestamp)
      {
        Id = dataForm.Id,
        FormItemId = dataForm.FormItemId,
        InteractionId = dataForm.SessionId,
        Data = dataForm.Data
      };

      List<Field> fields = new List<Field>();
      foreach (Infrastructure.Data.Field dataField in dataForm.Fields)
      {
        Field field = _fieldFactory.Create(dataField, form);
        if (field != null)
          fields.Add(field);
      }
      form.Field = fields;
      return form;
    }

    private readonly FieldFactory _fieldFactory = new FieldFactory();
  }
}