using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Accounts.Models
{
    public sealed class DbManager
    {
        #region "Members"
        private SqlCommand command;
        private SqlCommand _selectCommand;
        private SqlCommand _updateCommand;
        private SqlCommand _insertCommand;
        private int _BatchSize;
        private SqlDataAdapter commandAdapter;
        private SqlParameter commandParameter;
        private string connnectionString = string.Empty;
        private DataSet dsResults;
        #endregion
        private SqlCommandBuilder commandBuilder;

        #region "Properties"

        public SqlParameterCollection Parameters
        {
            get { return this.command.Parameters; }
        }

        public SqlCommand SelectCommand
        {
            get { return this._selectCommand; }
            set { this._selectCommand = value; }
        }

        public SqlCommand UpdateCommand
        {
            get { return this._updateCommand; }
            set { this._updateCommand = value; }
        }

        public SqlCommand InsertCommand
        {
            get { return this._insertCommand; }
            set { this._insertCommand = value; }
        }

        public int BatchSize
        {
            get { return this._BatchSize; }
            set { this._BatchSize = value; }
        }


        #endregion

        #region "Constructors"

        public DbManager()
        {
            this.command = new SqlCommand();
            this.command.CommandType = CommandType.StoredProcedure;

            this.SelectCommand = new SqlCommand();
            this.UpdateCommand = new SqlCommand();
            this.InsertCommand = new SqlCommand();
            this.SelectCommand.CommandType = CommandType.StoredProcedure;
        }

        public DbManager(string otherConnectionString)
        {
            this.connnectionString = otherConnectionString;
            this.command = new SqlCommand();
            this.SelectCommand = new SqlCommand();
            this.UpdateCommand = new SqlCommand();
            this.InsertCommand = new SqlCommand();
            this.command.CommandType = CommandType.StoredProcedure;
        }
        public DbManager(string otherConnectionString, string commandType)
        {
            this.connnectionString = otherConnectionString;
            this.command = new SqlCommand();
            this.SelectCommand = new SqlCommand();
            this.UpdateCommand = new SqlCommand();
            this.InsertCommand = new SqlCommand();
            this.command.CommandType = CommandType.Text;
        }

        public DbManager(DbTransaction objTransaction)
        {
            this.command = new SqlCommand();
            this.SelectCommand = new SqlCommand();
            this.UpdateCommand = new SqlCommand();
            this.InsertCommand = new SqlCommand();
            if ((objTransaction != null))
            {
                this.command.Transaction = objTransaction.Transaction;
                this.SelectCommand.Transaction = objTransaction.Transaction;
                this.UpdateCommand.Transaction = objTransaction.Transaction;
                this.InsertCommand.Transaction = objTransaction.Transaction;
            }
            this.command.CommandType = CommandType.StoredProcedure;
        }
        #endregion

        #region "Public Functions"

        public object ExecuteScalar(string commandText)
        {
            object returnValue = null;
            this.command.CommandText = commandText;
            try
            {
                OpenConnection();
                returnValue = this.command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return returnValue;
        }

        public int ExecuteNonQuery(string commandText)
        {
            int returnValue = 0;
            this.command.CommandText = commandText;
            try
            {
                OpenConnection();
                returnValue = this.command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return returnValue;
        }
        public int ExecuteNonQuery_Text(string commandText)
        {
            int returnValue = 0;
            this.command.CommandText = commandText;
            this.command.CommandType = CommandType.Text;
            try
            {
                OpenConnection();
                returnValue = this.command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return returnValue;
        }

        public SqlDataReader ExecuteReader(string commandText)
        {
            this.command.CommandText = commandText;
            try
            {
                OpenConnection();
                return this.command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public SqlDataReader ExecuteSingleRowReader(string commandText)
        {
            this.command.CommandText = commandText;
            try
            {
                OpenConnection();
                return this.command.ExecuteReader(CommandBehavior.SingleRow | CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            DataTable dtResults = new DataTable();
            this.commandAdapter = new SqlDataAdapter();
            this.command.CommandText = commandText;
            this.command.CommandTimeout = 400;
            this.commandAdapter.SelectCommand = this.command;
            try
            {
                OpenConnection();
                this.commandAdapter.Fill(dtResults);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dtResults;
        }

        public DataTable ExecuteDataTableQuery(string commandText)
        {
            DataTable dtResults = new DataTable();
            this._selectCommand = new SqlCommand();
            this._selectCommand.CommandText = commandText;
            this._selectCommand.CommandTimeout = 400;
            
            this.commandAdapter = new SqlDataAdapter();
                   
            try
            {
                OpenConnection();
                this._selectCommand.Connection = this.command.Connection;
                this.commandAdapter.SelectCommand = this._selectCommand;     
                this.commandAdapter.Fill(dtResults);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dtResults;
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            this.dsResults = new DataSet();
            this.commandAdapter = new SqlDataAdapter();
            this.command.CommandText = commandText;
            this.commandAdapter.SelectCommand = this.command;
            try
            {
                OpenConnection();
                this.commandAdapter.Fill(dsResults);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dsResults;
        }

        public int InsertDataTable(string commandText, DataTable dtRecords)
        {
            this.commandAdapter = new SqlDataAdapter();
            this.InsertCommand.CommandText = commandText;
            this.InsertCommand.CommandType = CommandType.StoredProcedure;
            this.commandAdapter.InsertCommand = this.InsertCommand;

            if ((this.BatchSize > 0))
            {
                this.commandAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
            }

            try
            {
                OpenConnection();
                return this.commandAdapter.Update(dtRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public int UpdateDataTable(DataTable dtRecords)
        {
            try
            {
                OpenConnection();
                //Intializing the select command            
                this.commandAdapter = new SqlDataAdapter(this.SelectCommand);
                this.commandAdapter.UpdateCommand = this.UpdateCommand;
                this.commandAdapter.InsertCommand = this.InsertCommand;

                //Intializing the command builder object for update
                this.commandBuilder = new SqlCommandBuilder(this.commandAdapter);
                return this.commandAdapter.Update(dtRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }


        public int UpdateDataTable(string selectCommandText, DataTable dtRecords)
        {
            this.command.CommandText = selectCommandText;
            this.commandAdapter = new SqlDataAdapter(this.command);
            this.commandBuilder = new SqlCommandBuilder(this.commandAdapter);
            try
            {
                OpenConnection();
                return this.commandAdapter.Update(dtRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            this.command.Parameters.AddWithValue(parameterName, parameterValue);
        }

        public void AddParameter(string parameterName, object parameterValue, SqlDbType parameterType, int size)
        {
            this.command.Parameters.Add(parameterName, parameterType, size).Value = parameterValue;
        }

        public void AddParameter(string parameterName, object parameterValue, SqlDbType parameterType, int size, ParameterDirection parameterDirection)
        {
            SqlParameter param = new SqlParameter(parameterName, parameterType, size);
            param.Direction = parameterDirection;
            param.Value = parameterValue;
            this.command.Parameters.Add(param);
        }

        public void AddParameter(string parameterName, object parameterValue, SqlDbType parameterType, int size, string sourceColumn)
        {
            this.command.Parameters.Add(parameterName, parameterType, size, sourceColumn).Value = parameterValue;
        }



        public void AddParameter(string parameterName, SqlDbType parameterType, int size, string sourceColumn)
        {
            this.command.Parameters.Add(new SqlParameter(parameterName, parameterType, size, sourceColumn));
        }

        public void BeginTransaction()
        {
            try
            {
                OpenConnection();
                this.command.Transaction = this.command.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw ex;
            }
        }

        public void RollBackTransaction()
        {
            try
            {
                this.command.Transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void CommitTransaction()
        {
            this.command.Transaction.Commit();
            this.CloseConnection();
        }

        public void OpenConnection()
        {
            if ((this.command.Connection == null))
            {
                if ((this.command.Transaction == null))
                {
                    this.command.Connection = this.EstablishConnection();
                    this.SelectCommand.Connection = this.command.Connection;
                    this.UpdateCommand.Connection = this.command.Connection;
                    this.InsertCommand.Connection = this.command.Connection;
                }
                else
                {
                    this.command.Connection = this.command.Transaction.Connection;
                    this.SelectCommand.Connection = this.command.Connection;
                    this.UpdateCommand.Connection = this.command.Connection;
                    this.InsertCommand.Connection = this.command.Connection;
                }
            }
            if (this.command.Connection.State == ConnectionState.Closed)
            {
                this.command.Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if ((this.command.Connection != null) && this.command.Connection.State != ConnectionState.Closed)
            {
                if ((this.command.Transaction == null))
                {
                    this.command.Connection.Close();
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (this.command.Connection.State != ConnectionState.Closed)
            {
                this.command.Connection.Close();
            }
            this.command = null;
            this.commandAdapter = null;
        }

        #endregion

        #region "Private Functions"

        private SqlConnection EstablishConnection()
        {
            SqlConnection connBMS = new SqlConnection();
            if ((string.IsNullOrEmpty(this.connnectionString)))
            {
                connBMS.ConnectionString = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
            }
            else
            {
                connBMS.ConnectionString = ConfigurationManager.ConnectionStrings[connnectionString].ConnectionString;
            }
            return connBMS;
        }

 
        #endregion

    }
}