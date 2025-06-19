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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace D032310392
{
    public partial class FormBook : Form
    {
        public FormBook()
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
            new FormStock().Show();
            this.Hide();
        }
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\USER\\Desktop\\DITP2123\\LabTest2\\D032310392\\D032310392\\AdmiralBookstoreDatabase.mdf;Integrated Security=True";

        private void FormBook_Load(object sender, EventArgs e)
        {
            LoadAuthorData();
        }
        private void LoadAuthorData()
        {
            string query = "SELECT * FROM Book";

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
                cmd.CommandText = @"UPDATE [Book] 
                        SET [ISBN-13] = @ISBN, 
                            Title = @Title, 
                            Publisher = @Publisher, 
                            PublishDate = @PublishDate 
                        WHERE [ISBN-13] = @ISBN";

                DateTime publishDate = DateTime.Parse(textBox4.Text).Date;

                // New values
                cmd.Parameters.AddWithValue("@ISBN", textBox1.Text);
                cmd.Parameters.AddWithValue("@Title", textBox2.Text);
                cmd.Parameters.AddWithValue("@Publisher", textBox3.Text);
                cmd.Parameters.AddWithValue("@PublishDate", publishDate);

                cmd.ExecuteNonQuery();
                connection.Close();
            }


            textBox1.Text = "";

            MessageBox.Show("Data updated successfully");
            LoadAuthorData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["ISBN-13"].Value.ToString();
                textBox2.Text = row.Cells["Title"].Value.ToString();
                textBox3.Text = row.Cells["Publisher"].Value.ToString();
                textBox4.Text = row.Cells["PublishDate"].Value.ToString();
            }
        }
    }
}

