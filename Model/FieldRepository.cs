using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Forms.Data;

namespace WFFM.SQLServer.SaveToDatabase.Model
{
    class FieldRepository
    {
        public IField Get(IForm form, Guid fieldId)
        {
            return form == null ? null : form.Field.FirstOrDefault(field => field.FieldId == fieldId);
        }
    }
}
