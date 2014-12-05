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
using System.Data;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Data.Submit;
using Sitecore.Form.Submit;
using WFFM.SQLServer.SaveToDatabase.Model;

namespace WFFM.SQLServer.SaveToDatabase.Infrastructure.Actions
{
    public class SaveToDatabase : ISaveAction, ISubmit
    {
        public virtual void Execute(ID formId, AdaptedResultList fields, object[] data)
        {
            Assert.ArgumentNotNull(formId, "formid");
            Assert.ArgumentNotNull(data, "data");
            try
            {
                ID sessionId = ID.Null;
                if ((data != null) && (data.Length > 0))
                {
                    ID.TryParse(data[0], out sessionId);
                }

                _formReposiotry.Insert(formId, fields, sessionId,
                  ((data != null) && (data.Length > 1)) ? data[0].ToString() : null);
            }
            catch (EntityCommandExecutionException entityCommandExecutionException)
            {
                Exception innerException = entityCommandExecutionException;
                if (entityCommandExecutionException.InnerException != null)
                {
                    innerException = entityCommandExecutionException.InnerException;
                }
                Log.Error("Save To Database failed.", innerException, innerException);
                throw innerException;
            }
            catch (Exception exception)
            {
                Log.Error("Save To Database failed.", exception, exception);
                throw;
            }
        }

        [Obsolete("Use Execute instead.")]
        public void Submit(ID formid, AdaptedResultList fields)
        {
            Execute(formid, fields, null);
        }

        private readonly FormRepository _formReposiotry = new FormRepository();
    }
}