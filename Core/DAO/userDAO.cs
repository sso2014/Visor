﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.VO;
using System.Data;

namespace Core.DAO
{
    public class userDAO
    {
        public userDAO()
        {
          conn = new dbConnection();
        }

        private dbConnection conn;
    
        public DataTable GetEquipos()
        {
            string query = string.Format(
                "READ_ALL_EQUIPO");
            return conn.executeSelectQuery(query, null);
        }
    }
}