using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace Core
{
    public class conn
    {

        private string StringConnection = "";

        public OleDbConnection GetOldbConnection()
        {
            try
            {

                OleDbConnection con = new OleDbConnection();
                
                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    return con;
                }
                else {
                    return null;
                }
            }
            catch (Exception exp) {
                return null;
            }
        }
    }
}