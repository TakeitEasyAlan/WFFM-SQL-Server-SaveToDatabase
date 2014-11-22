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
using System.Text.RegularExpressions;
using Sitecore.Form.Core.Controls.Data;

namespace WFFM.SQLServer.SaveToDatabase.Model
{
  internal class FieldFactory
  {
    public Field Create(Infrastructure.Data.Field dataField, Form form)
    {
      if (dataField == null)
        return null;

      return new Field
      {
        Id = dataField.Id,
        FieldId = dataField.FieldId,
        Form = form,
        Value = dataField.Value,
        Data = dataField.Data,
        FieldName = dataField.FieldName
      };
    }

    public Infrastructure.Data.Field Create(AdaptedControlResult adaptedControlResult)
    {
      if (adaptedControlResult == null)
        return null;

      return new Infrastructure.Data.Field
      {
        Id = Guid.NewGuid(),
        FieldId = new Guid(adaptedControlResult.FieldID),
        FieldName = adaptedControlResult.FieldName,
        Data = adaptedControlResult.Parameters ?? string.Empty,
        Value = ((adaptedControlResult.Parameters != null) && adaptedControlResult.Parameters.StartsWith("secure:"))
          ? Regex.Replace(adaptedControlResult.Parameters, "<schidden>.*</schidden>", "<schidden></schidden>")
          : adaptedControlResult.Value
      };
    }

  }
}