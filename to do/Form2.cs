using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;


namespace to_do
{
    public partial class Form2 : Form
    {
        private DataGridView dataGridView1Form1;
        private int rowCount;
        public MySqlConnection conn;
        public List<string> categorys;
        int userid;
        private Form1 form1;
        string title="";
        Dictionary<string, int> categoryID;
        public Form2(Form1 form1, DataGridView dataGridView1Form1)
        {
            this.dataGridView1Form1 = dataGridView1Form1;

            InitializeComponent();
            this.form1 = form1;
            dataGridView1.ReadOnly = false;
            categorys = form1.TakeCategory();
            userid = form1.userid;
            conn = form1.conn;


            categoryID = form1.GetCategories();
            dataGridView1.Rows.Clear();
            foreach (string category in categorys)
            {
              // Console.WriteLine(category);
                dataGridView1.Rows.Add(category);
            }
            dataGridView1.Refresh();
            rowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }//bos
        private void addcatebtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("Column Name");
            int rowIndex = dataGridView1.Rows.Count - 1; // Yeni eklenen satırın indexi
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0]; // Yeni eklenen hücreyi seç
            dataGridView1.ReadOnly=false; // Hücreyi düzenlenebilir hale getir

        }
        private void delbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0) // en az bir hücre seçili mi?
            {
                MySqlConnection conn;
                conn = form1.conn;
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex; // seçilen hücrenin satır indeksi
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex]; // seçilen satır
                string cellValue = selectedRow.Cells[0].Value.ToString(); // seçilen satırdaki ilk hücrenin değeri
                Console.WriteLine(cellValue);
                string sql = $"DELETE FROM category WHERE category_name='{cellValue}'";
               


                DialogResult result= MessageBox.Show("olan tum gorevler silinecektir","are you sure",MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //Console.Clear();
                    Console.WriteLine($"{rowsAffected} kayıt silindi.");
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex); // Seçili satırı sil
                    Form1 form = (Form1)Application.OpenForms["Form1"];
                    form.RefreshDataGridView();

                }
            }
        } //secilen asatırı ve altındaki tum gorevleri siler
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Console.WriteLine("edit mode is active");
            title = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Console.WriteLine("basılsan categorinin title bilgisi: ", title.ToUpper());
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
            dataGridView1.BeginEdit(true);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= rowCount)
            {
                // Yeni bir kategori eklenmişse
                try
                {
                    Console.WriteLine("yeni row");

                    string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string sql = $"INSERT INTO category (userid,category_name) VALUES({userid},'{newValue}')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} kayıt eklendi.");
                    categoryID = form1.GetCategories();

                    form1.RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: Kategori eklenirken bir hata oluştu");
                    Console.WriteLine("Hata Detayı: " + ex.Message);
                }
            }
            else
            {
                // Mevcut bir kategori düzenlenmişse
                try
                {
                    Console.WriteLine("mevcut yer");
                    int ID = categoryID[title];
                    string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    string sql = $"UPDATE  category SET category_name='{newValue}' WHERE id={ID}";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} kayıt güncellendi.");
                    categoryID = form1.GetCategories();
                    form1.RefreshDataGridView();
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine("Hata: Kategori bulunamadı");
                    Console.WriteLine("Hata Detayı: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: Kategori güncellenirken bir hata oluştu");
                    Console.WriteLine("Hata Detayı: " + ex.Message);
                }
            }
            rowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);

        }


        //private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        int ID = categoryID[title];
        //        string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        //        string sql = $"UPDATE  category SET category_name='{newValue}' WHERE id={ID}";
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        int rowsAffected = cmd.ExecuteNonQuery();
        //        Console.WriteLine($"{rowsAffected} kayıt guncellendi.");
        //        categoryID = form1.GetCategories();
        //        form1.RefreshDataGridView();

        //    }
        //    catch (System.Collections.Generic.KeyNotFoundException)//bulamassa
        //    {
        //        Console.WriteLine(" cahtch save kısmkı");

        //        int totalRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);//sadece görunen rows sayar 

        //        for (int i = rowCount; i < totalRowCount; i++)
        //        {
        //            string row = dataGridView1.Rows[i].Cells[0].Value.ToString();
        //            string sql = $"INSERT INTO category (userid,category_name) VALUES({userid},'{row}')";
        //            MySqlCommand cmd = new MySqlCommand(sql, conn);
        //            int rowsAffected = cmd.ExecuteNonQuery();
        //            //Console.Clear();
        //            Console.WriteLine($"{rowsAffected} kayıt eklendi.");
        //        }
        //        //dataGridView1Form1.Refresh();
        //        Form1 form = (Form1)Application.OpenForms["Form1"];
        //        categoryID = form1.GetCategories();

        //        form.RefreshDataGridView();
        //    }



        //    /*
        //    //    MySqlConnection conn;
        //    //    conn = form1.conn;
        //    //    int totalRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);//sadece görunen rows sayar 

        //    //    for (int i = rowCount; i < totalRowCount; i++)
        //    //    {
        //    //        string row = dataGridView1.Rows[i].Cells[0].Value.ToString();
        //    //        string sql = $"INSERT INTO category (userid,category_name) VALUES({userid},'{row}')";
        //    //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    //        int rowsAffected = cmd.ExecuteNonQuery();
        //    //        //Console.Clear();
        //    //        Console.WriteLine($"{rowsAffected} kayıt eklendi.");

        //    //    }
        //    //    //dataGridView1Form1.Refresh();
        //    //    Form1 form = (Form1)Application.OpenForms["Form1"];
        //    //    form.RefreshDataGridView();
        //    //
        //    */

        //}
        private void savebtn1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("save kısmkı");
           
            int totalRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);//sadece görunen rows sayar 

            for (int i = rowCount; i < totalRowCount; i++)
            {
                string row = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string sql = $"INSERT INTO category (userid,category_name) VALUES({userid},'{row}')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                //Console.Clear();
                Console.WriteLine($"{rowsAffected} kayıt eklendi.");
            }
            //dataGridView1Form1.Refresh();
            Form1 form = (Form1)Application.OpenForms["Form1"];
            categoryID = form1.GetCategories();
            rowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);

            form.RefreshDataGridView();

        }
    }
}