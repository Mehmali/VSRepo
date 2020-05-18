using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Accounts.Models
{
    public class DbTransaction
    {


        private string DbConnectionStringName;
        private SqlTransaction _Transaction;
        public SqlTransaction Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }

        #region "Constructors"

        public DbTransaction()
        {
        }
        public DbTransaction(string connectionStringName)
        {
            DbConnectionStringName = connectionStringName;
        }
        #endregion

        #region "Methods"
        public void StartTransaction()
        {
            SqlConnection objConnection = EstablishConnection();
            objConnection.Open();
            this.Transaction = objConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this.Transaction.Commit();
        }

        public void RollBackTransaction()
        {
            this.Transaction.Rollback();
        }

        private SqlConnection EstablishConnection()
        {
            SqlConnection connBMS = new SqlConnection();
            if ((string.IsNullOrEmpty(DbConnectionStringName)))
            {
                connBMS.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            }
            else
            {
                connBMS.ConnectionString = ConfigurationManager.ConnectionStrings[DbConnectionStringName].ConnectionString;
            }
            return connBMS;
        }
        #endregion

    }
}