using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.VO;
using Core.DAO;

namespace Core.BUS
{
    public class userBUS
    {
        public userBUS()
        {
           userDao = new userDAO();
        }

        userDAO userDao;

        public List<Equipo> GetEquipo()
        {
            List<Equipo> equipos = new List<Equipo>();

            foreach (DataRow dr in userDao.GetEquipos().Rows)
            {
                if (dr["ID"].ToString().Substring(0, 2) == "P_")
                {
                    equipos.Add(
                    new Pivot() { ID = (string)dr["ID"], Panel = new Panel() { ID = (string)dr["ID"] }});
                }
                else if (dr["ID"].ToString().Substring(0, 2) == "F_")
                {
                    equipos.Add(
                    new Frontal() {  ID = (string)dr["ID"], Panel = new Panel() { ID = (string)dr["ID"] }});
                }
            }
            return equipos;
        }
    }
}