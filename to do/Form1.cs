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
using MySql.Data.MySqlClient;

/*
    YAPILACAKLAR
*SQL GEREKLİ YERLERE TASINCAK
*MONTH OLAYI OTO ALINACAK 
*ID EKLENEK GOREVLERE VE LİSTELENCEK 
 */


namespace to_do
{
    public partial class Form1 : Form
    {
        Timer timer1 = new Timer();

        public MySqlConnection Connect()
        {
            string connectionString = "server=localhost;user id=root;password=1234;database=todo";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                status.Text = "bağlandı";
                Console.WriteLine("MySQL version : {0}", connection.ServerVersion);
                return connection;

          
            }
            catch (Exception ex)
            {
                status.Text = "olmadı";
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public void Print(MySqlConnection connection)
        {
            string queryString = "SELECT title,time,status FROM gorevler";
            using (MySqlCommand command = new MySqlCommand(queryString, connection))
            {
                MySqlDataReader reader = command.ExecuteReader();

             
                while (reader.Read())
                {
                    DateTime myDateTime = (DateTime)reader["time"]; // Veriyi DateTime tipine dönüştürün
                    string formattedDateTime = myDateTime.ToString("dd/MM/yyyy hh:mm:ss"); // Veriyi istediğiniz formata dönüştürün
                    Console.WriteLine(String.Format("{0}, {1} ,{2}", reader[0], formattedDateTime, reader[2]));
                }
                reader.Close();
            }
        }
        public void Write(MySqlConnection connection, string gorev, DateTime time)//buraya category, user  tablo yapıldıktan sonra oto gelecek sonra degerleri kullanıcı verecek  
        {
            string timeFormatted = time.ToString("yyyy-MM-dd HH:mm:ss");
            string queryString = $"INSERT INTO gorevler (category_id, title, time, status) VALUES (1, '{gorev}', '{timeFormatted}', 0)";
            using (MySqlCommand command = new MySqlCommand(queryString, connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " kayıt eklendi.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            label2.Text = now.ToString("dd/MM/yyyy hh:mm:ss");
        }
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000; // 1 saniye
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            MySqlConnection connection = Connect();
            if (connection != null)
            {
                DateTime bugununTarihi = DateTime.Now;
                Write(connection, "2. gorev", bugununTarihi);
                //Print(connection);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            
        }





























        private void label2_Click(object sender, EventArgs e)
        {}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
