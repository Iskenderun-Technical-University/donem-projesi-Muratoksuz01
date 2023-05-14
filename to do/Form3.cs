using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace to_do
{
    public partial class Form3 : Form
    {
        string sql;
        public int userid;
        public string username1;

        public Form3()
        {
            InitializeComponent();
             
            
        }
        public MySqlConnection Connect()
        {
            string connStr = "server=localhost;user=root;database=todo;port=3306;password=1234;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                // Veritabanı bağlantısını aç
                conn.Open();
                return conn;

            }
            catch (Exception ex)
            {
                // Bağlantı hatası oluştuysa burada işlemler yapılabilir
                Console.WriteLine("Hata: " + ex.ToString());
                return null;
            }
        }

        private void lbgiris_Click(object sender, EventArgs e)
        {// login 
           lbheader.Visible = false;
            lbinfo.Visible = false;
            lblink.Visible = false;
            lbpass.Visible = false;
            lbemail.Visible = false;
            lbuser.Visible = false;
            btnkayit.Visible = false;

            txtrepass.Visible = false;
            txtreuser.Visible = false;
            txtreemail.Visible = false;


            // kayıt 
            lblheader.Visible = true;
            lblinfo.Visible = true;
            lbllink.Visible = true;
            lblpass.Visible = true;
            lbluser.Visible = true;
            btngiris.Visible = true;

            txtlouser.Visible = true;
            txtlopass.Visible = true;
            //txtloemail.Visible = true;

        }
        private void lbllink_Click(object sender, EventArgs e)//login text link
        {// kayıt 
            lblheader.Visible = false;
            lblinfo.Visible = false;
            lbllink.Visible = false;
            lblpass.Visible = false;
            lbluser.Visible = false;
            btngiris.Visible = false;

            txtlouser.Visible = false;
            txtlopass.Visible = false;
            //txtreemail.Visible = false;

            // logib
            lbheader.Visible = true;
            lbinfo.Visible = true;
            lblink.Visible = true;
            lbpass.Visible = true;
            lbemail.Visible = true;
            lbuser.Visible = true;
            btnkayit.Visible = true;

            txtrepass.Visible = true;
            txtreuser.Visible = true;
            txtreemail.Visible = true;
        }
        private void btnkayit_Click(object sender, EventArgs e)
        {
            //kayıt olacak 
            MySqlConnection conn;
            conn = Connect();
            string username = txtreuser.Text;
            string password = txtrepass.Text;
            string email= txtreemail.Text;
          

            if(email!="" &&username!="" && password != "")
            {
                sql = $"INSERT INTO users (username,passwd,email) VALUES ('{username}','{password}','{email}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected != 0)
                {
                    MessageBox.Show("basarılı");
                    // login 
                    lbheader.Visible = false;
                    lbinfo.Visible = false;
                    lblink.Visible = false;
                    lbemail.Visible = false;
                    lbpass.Visible = false;
                    lbuser.Visible = false;
                    btnkayit.Visible = false;

                    txtrepass.Visible = false;
                    txtreuser.Visible = false;
                    txtreemail.Visible = false;


                    // kayıt 
                    lblheader.Visible = true;
                    lblinfo.Visible = true;
                    lbllink.Visible = true;
                    lblpass.Visible = true;
                    lbluser.Visible = true;
                    btngiris.Visible = true;

                    txtlouser.Visible = true;
                    txtlopass.Visible = true;

                }
                else MessageBox.Show("olmadı ");
                //Console.Clear();
                Console.WriteLine($"{rowsAffected} kayıt eklendi.");
            }
            else
            {
                MessageBox.Show("bilgileri doldurunuz");
            }
            
           

        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            conn = Connect();
            string username=txtlouser.Text;
            string password=txtlopass.Text;
            sql = $"SELECT * FROM users WHERE username='{username}' and passwd='{password}'";
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine(reader.ToString());
                    if (reader.HasRows) 
                    {

                        while (reader.Read())
                        {
                            // kayıt varsa ilgili verileri burada kullanabilirsiniz
                            userid = (int)reader["id"];
                            username1 = reader.GetString("username");
                        }



                        Form1 form1 = new Form1(this);
                        form1.Show();
                        this.Hide();
                        Console.WriteLine(userid.ToString()+"   "+ username1);
                    }
                    else
                    {
                        MessageBox.Show("verdiğiniz bilgilere gore kişi yok");
                    }
                }
            }




        }
    }
}
