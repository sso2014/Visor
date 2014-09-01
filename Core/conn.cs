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
        
        private string StringConnection = @"Data Source=TELEMETRIA-PC\WINCC;Initial Catalog=Visor;Integrated Security=True";

        public SqlConnection GetConnection() {

            SqlConnection con = new SqlConnection(StringConnection);
            
            try
            {
                con.Open();

                if (con.State == ConnectionState.Open) {
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