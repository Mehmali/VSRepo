using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounts.Models
{
    public static class ErrorLog
    {
        public static void LogError(string ErrorMessage, string StackTrace, string Source, string MethodName, string AdditionalInfo)
        {
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            //string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            string myIP = HttpContext.Current.Request.UserHostAddress;

            DbManager objDbManager = new DbManager("ApplicationErrorLog");
            objDbManager.AddParameter("@ErrorMessage", ErrorMessage);
            objDbManager.AddParameter("@Browser", DBNull.Value);
            objDbManager.AddParameter("@StackTrace", StackTrace);
            objDbManager.AddParameter("@Source", Source);
            objDbManager.AddParameter("@MethodName", MethodName);
            objDbManager.AddParameter("@IP", myIP);
            if (string.IsNullOrEmpty(AdditionalInfo))
            {
                objDbManager.AddParameter("@AdditionalInfo", DBNull.Value);
            }
            else
            {
                objDbManager.AddParameter("@AdditionalInfo", AdditionalInfo);
            }

            objDbManager.AddParameter("@ApplicationName", "MachineManagementSystem");
            objDbManager.ExecuteNonQuery("ErrorLog_Add");

        }
    }
}