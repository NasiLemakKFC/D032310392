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
    public partial class FormAuthor : Form
    {
        public FormAuthor()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FormBook().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FormStock().Show();
            this.Hide();
        }
        string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\USER\\Desktop\\DITP2123\\LabTest2\\D032310392\\D032310392\\AdmiralBookstoreDatabase.mdf;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"INSERT INTO [Author] (AuthorID, Name, BirthYear)
                     VALUES (@AuthorID, @Name, @BirthYear)";

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AuthorID", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@BirthYear", textBox3.Text);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                LoadAuthorData();
            }
        }

        private void FormAuthor_Load(object sender, EventArgs e)
        {
            LoadAuthorData();
        }
        private void LoadAuthorData()
        {
            string query = "SELECT * FROM Author";

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

    }
}
