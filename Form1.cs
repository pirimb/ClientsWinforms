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
using System.Configuration;
using System.Data;

namespace testApp
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ClientsDB"].ConnectionString);

            sqlConnection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Clients", sqlConnection);

            DataSet db = new DataSet();

            dataAdapter.Fill(db);

            dataGridView2.DataSource = db.Tables[0];
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO Clients (name,surname,patronymic,email,phone,pin) VALUES(@name,@surname,@patronymic,@email,@phone,@pin)",
                sqlConnection);

            command.Parameters.AddWithValue("name", textBoxName.Text);
            command.Parameters.AddWithValue("surname", textBoxSurname.Text);
            command.Parameters.AddWithValue("patronymic", textBoxPatron.Text);
            command.Parameters.AddWithValue("email", textBoxMail.Text);
            command.Parameters.AddWithValue("phone", textBoxPhone.Text);
            command.Parameters.AddWithValue("pin", textBoxPin.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }


        private void buttonSelect_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                textBoxSelect.Text,
                sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"name like '%{textBoxFilter.Text}%'";
        }
    }
}
