using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Kursovay_Lisy
{
    public class DB_Conector
    {
        public  DataSet ds;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\яяяяяяяя\Desktop\SAASZ\C#\Kursovay_Lisy\Kursovay_Lisy\bd_curs.mdf;Integrated Security=True";
        public SqlDataAdapter adapter;
        public string nameTable;
        public string reqvest_sql;

        public void Updata()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = reqvest_sql;
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
            }
        }

        public List<string> Qwest(string query)
        {
            List<string> strings = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);
                 connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string result = string.Join(" ", Enumerable.Range(0, reader.FieldCount)
                .Select(i => reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString()));
                    strings.Add(result);
                }
            }

            return strings;

        }

        public void fill_dataView (string reqvest_sql, DataGridView dataView)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                this.reqvest_sql = reqvest_sql;
                connection.Open();
                adapter = new SqlDataAdapter(reqvest_sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataView.DataSource = ds.Tables[0];
            }
        }
        public DB_Conector()
        {
        }
    }


}
