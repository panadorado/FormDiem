using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSDL_DT
{
    class Function_Connect
    {
        public static SqlConnection conn;
        public static void connect()
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-NSOO413\SQLEXPRESS;Initial Catalog=CSDL_TD;Integrated Security=True");
            conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Ket noi thanh cong !");
            }
            else MessageBox.Show("Ket noi that bai !");
        }

        public static void disconnect()
        {
            //conn = new SqlConnection("Data Source=DESKTOP-NSOO413\\SQLEXPRESS;Initial Catalog=quanlybanhang;Integrated Security=True");
            //conn.Open();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public static DataTable getdata(String sql)
        {
            SqlDataAdapter my_sql = new SqlDataAdapter(sql, conn);
            my_sql.SelectCommand = new SqlCommand();
            my_sql.SelectCommand.Connection = conn;
            my_sql.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            my_sql.Fill(table);

            return table;
        }

        public static void runsql(String sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public static bool checkkey(String sql)
        {
            SqlDataAdapter my_sql = new SqlDataAdapter(sql, conn);
            my_sql.SelectCommand = new SqlCommand();
            my_sql.SelectCommand.Connection = conn;
            my_sql.SelectCommand.CommandText = sql;
            DataTable table = new DataTable();
            my_sql.Fill(table);

            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
    }
}
