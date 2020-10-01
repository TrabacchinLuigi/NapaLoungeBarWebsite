using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Napa.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static String Index => "Index";

        public static String Email => "Email";

        public static String ChangePassword => "ChangePassword";

        public static String DownloadPersonalData => "DownloadPersonalData";

        public static String DeletePersonalData => "DeletePersonalData";

        public static String ExternalLogins => "ExternalLogins";

        public static String PersonalData => "PersonalData";

        public static String TwoFactorAuthentication => "TwoFactorAuthentication";

        public static String IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static String EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static String ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static String DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        public static String DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

        public static String ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static String PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static String TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static String PageNavClass(ViewContext viewContext, String page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as String
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return String.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
