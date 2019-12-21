using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Threading.Tasks;

namespace WPF_APP_CORE_Restaurant
{
    public class DataBaseSaource
    {
        const string CONNECTION_PATH = @"Data Source=WIN-11AASOPTNC3;Initial Catalog=Restaurant;Integrated Security=True";
        public bool DEBUG { get; set; } = false;
        public SqlConnection Connection { get; set; }
        public DataBaseSaource()
        {
            try
            {
                this.Connection = new SqlConnection(CONNECTION_PATH);
                if (DEBUG)
                {
                    MessageBox.Show($"Connecteed: {Connection.State}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public DataTable Select(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, Connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public void query(string query)
        {
            Connection.Open();
            SqlCommand command = new SqlCommand(query, connection: Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }
        public int ScalarQuery(string query)
        {
            Connection.Open();
            SqlCommand command = new SqlCommand(query, connection: Connection);
            int result = command.ExecuteNonQuery();
            Connection.Close();
            return result;
        }
        public DataTable GetTable(string name)
        {
            return Connection.GetSchema(name);
        }
    }
}
