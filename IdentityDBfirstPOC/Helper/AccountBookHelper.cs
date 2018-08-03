using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityDBfirstPOC.Helper
{
    public static class AccountBookHelper
    {

        public static HtmlString DisplayCategory(this HtmlHelper htmlHelper, int Categoryyy)
        {
          
            if (Categoryyy == 1)
            {
                return new MvcHtmlString("<span class='text-info'>收入</span>");
            }
            else
            {
                return new MvcHtmlString("<span class='text-danger'>支出</span>");
            }
            
        }
    }
}