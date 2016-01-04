using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Helper
{
    public class AttributeTool : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return this.ErrorMessage ?? string.Format("{0} 欄位請輸入正確的", name);
        }
    }
}