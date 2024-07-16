using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDB();
        }
        public async void LoadDB()
        {
            string connectionString =
                "Server = DAVID-ADLER;" +
                "Database = test;" +
                "User Id = sa;" +
                "Password =12345;" +
                "Trusted_Connection =true;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Table_1", conn);
                    DataTable mytable = new DataTable();
                    await Task.Run(() => adapter.Fill(mytable));
                    dataGridView1.DataSource = mytable;

                    await conn.OpenAsync();
                    SqlCommand comm = new SqlCommand("SELECT * FROM Table_1", conn);
                    SqlDataReader reader = await comm.ExecuteReaderAsync();
                    DataTable mytable1 = new DataTable();
                    mytable1.Load(reader);
                    dataGridView1.DataSource = mytable1;
                  

                }
                catch (Exception ex)
                {
                    MessageBox.Show("an error occurred:" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }
        public async void LoadDB2()
        {
            string connectionString =
                "Server = DAVID-ADLER;" +
                "Database = test;" +
                "User Id = sa;" +
                "Password =12345;" +
                "Trusted_Connection =true;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    //מיועד לשאילתות select והזרקת נתונים ישר לטבלה.
                    await conn.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Table_1", conn);
                    DataTable mytable = new DataTable();
                    await Task.Run(() => adapter.Fill(mytable));
                    dataGridView1.DataSource = mytable;
                    //מיועד לשאילתות פשוטות שמחזיר נתון בודד או לקריאה תורנית
                    await conn.OpenAsync();
                    SqlCommand comm = new SqlCommand("שאילתה", conn);
                    SqlDataReader reader = await comm.ExecuteReaderAsync();
                    DataTable mytable1 = new DataTable();
                    mytable1.Load(reader);
                    dataGridView1.DataSource = mytable1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("an error occurred:" + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

    }
}
