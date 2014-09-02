using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Core
{

    public class dbConnection
    {
        
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        private string StringConnection = @"Data Source=TELEMETRIA-PC\WINCC;Initial Catalog=Visor;Integrated Security=True";
        //private string StringConnection = @"Data Source=TELEMETRIA-PC;Initial Catalog=Visor;Integrated Security=True";
        //private string StringConnection = @"Data Source=SSO;Initial Catalog=Visor;Integrated Security=True";
        
        public dbConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(StringConnection);
        }
        private SqlConnection openConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
                {
                    conn.Open();
                }

                return conn;
            }
            catch (Exception exp)
            {
                return null;
            }
        }
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable datatable = new DataTable();
            //datatable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                myCommand.Parameters.Add(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                datatable = ds.Tables[0];
            }
            catch (SqlException Exp)
            {
            }
            finally
            {
            }

            return datatable;
        }
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException exp)
            {
                return false;
            }
            finally
            {
            }

            return true;
        }
    }
}