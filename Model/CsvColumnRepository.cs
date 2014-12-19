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
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Diagnostics;
using Sitecore.Forms.Data;

namespace WFFM.SQLServer.SaveToDatabase.Model
{
    class CsvColumnRepository
    {
        internal Dictionary<Guid, string> Get(IEnumerable<IForm> forms)
        {
            Assert.ArgumentNotNull(forms, "froms");
            Dictionary<Guid, string> columns = new Dictionary<Guid, string>();
            //we have to iterate over all rows as fields can be added and remove over time
            foreach (IForm form in forms)
            {
                foreach (IField field in form.Field.Where(field => !columns.ContainsKey(field.FieldId)))
                {
                    columns.Add(field.FieldId, field.FieldName);
                }
            }
            return columns;
        }

    }
}
