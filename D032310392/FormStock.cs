using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace D032310392
{
    public partial class FormStock : Form
    {
        public FormStock()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FormAuthor().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FormBook().Show(); this.Hide();
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            LoadAuthorData();
        }
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\USER\\Desktop\\DITP2123\\LabTest2\\D032310392\\D032310392\\AdmiralBookstoreDatabase.mdf;Integrated Security=True";

        private void LoadAuthorData()
        {
            string query = "SELECT * FROM Stock";

            using (SqlConnection con = new SqlConnection(conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Delete [Stock]
                        WHERE [StockID] = @stockID";

                // New values
                cmd.Parameters.AddWithValue("@stockID", textBox1.Text);

                cmd.ExecuteNonQuery();
                connection.Close();
            }


            textBox1.Text = "";
            MessageBox.Show("Data deleted successfully");
            LoadAuthorData();
        }
    }
}
