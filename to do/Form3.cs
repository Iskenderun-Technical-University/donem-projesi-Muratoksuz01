using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace to_do
{
    public partial class Form3 : Form
    {
        private Form1 form1;
       
        string sql;


        public Form3(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            
        }

        private void lbgiris_Click(object sender, EventArgs e)
        {// login 
           lbheader.Visible = false;
            lbinfo.Visible = false;
            lblink.Visible = false;
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
            conn = form1.Connect();
            string username = txtreuser.Text;
            string password = txtrepass.Text;
            string email= txtreemail.Text;
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

        private void btngiris_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            conn = form1.Connect();
            string username=txtlouser.Text;
            string password=txtlopass.Text;
            sql = $"SELECT * FROM users WHERE username='{username}' and passwd='{password}'";
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine(reader.ToString());
                    if (reader!=null) 
                    {
                        //Form1 form = new Form1();
                        form1.Show();
                        this.Hide();
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
