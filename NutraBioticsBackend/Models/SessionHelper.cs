using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace NutraBioticsBackend.Models
{
    public class SessionHelper
    {

        /// <summary>
        /// Encapsulates the session state
        /// </summary>
        public sealed class LoginInfo
        {
            private HttpSessionState _session;
            public LoginInfo(HttpSessionState session)
            {
                this._session = session;
            }

            public string Username
            {
                get { return (this._session["Username"] ?? string.Empty).ToString(); }
                set { this._session["Username"] = value; }
            }

            public string VendorId
            {
                get { return (this._session["VendorId"] ?? string.Empty).ToString(); }
                set { this._session["VendorId"] = value; }
            }

            public string Company
            {
                get { return (this._session["Company"] ?? string.Empty).ToString(); }
                set { this._session["Company"] = value; }
            }

        }
    }
}