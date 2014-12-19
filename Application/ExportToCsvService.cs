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
using System.Linq;
using System.Text;
using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Forms.Data;
using WFFM.SQLServer.SaveToDatabase.Model;

namespace WFFM.SQLServer.SaveToDatabase.Application
{
    public class ExportToCsvService
    {
        internal void ExportToCsv(HttpResponse response, ID formId, string formName)
        {
            Assert.ArgumentNotNull(response, "response");
            Assert.ArgumentNotNull(response, "formId");

            IEnumerable<IForm> forms = _formRepository.Get(formId).ToList();
            if (!forms.Any())
            {
                response.Write(string.Format("No data for form with id:{0}", formId));
                return;
            }


            StringBuilder csvString = new StringBuilder();
            Dictionary<Guid, string> columns = _csvColumnRepository.Get(forms);
            AddHeader(csvString, columns);
            AddRows(csvString, forms, columns);
            GenerateCsvResponseService.GenerateCsvResponse(response, formName, csvString.ToString());
        }



        private void AddRows(StringBuilder csvString, IEnumerable<IForm> forms, Dictionary<Guid, string> columns)
        {
            Assert.ArgumentNotNull(csvString, "csvString");
            Assert.ArgumentNotNull(forms, "forms");
            Assert.ArgumentNotNull(columns, "columns");

            foreach (IForm form in forms)
            {
                StringBuilder row = new StringBuilder();
                foreach (KeyValuePair<Guid, string> column in columns)
                {
                    string value = string.Empty;
                    IField field = _fieldRepository.Get(form, column.Key);
                    if (field != null)
                        value = CsvEncodeService.CsvEncode(field.Value);
                    if (row.Length > 0)
                        row.Append(Constants.Delimiter.Column);
                    row.Append(value);
                }
                csvString.AppendLine(row.ToString());
            }
        }


        private void AddHeader(StringBuilder csvString, Dictionary<Guid, string> columns)
        {
            Assert.ArgumentNotNull(csvString, "csvString");
            Assert.ArgumentNotNull(columns, "columns");

            StringBuilder header = new StringBuilder();
            foreach (KeyValuePair<Guid, string> column in columns)
            {
                if (header.Length > 0)
                    header.Append(Constants.Delimiter.Column);
                header.Append(column.Value);
            }
            csvString.AppendLine(header.ToString());
        }


        private readonly CsvColumnRepository _csvColumnRepository = new CsvColumnRepository();
        private readonly FieldRepository _fieldRepository = new FieldRepository();
        private readonly FormRepository _formRepository = new FormRepository();
    }

}
