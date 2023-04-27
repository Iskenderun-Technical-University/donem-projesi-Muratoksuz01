﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
/*
    YAPILACAKLAR
*
*takvim kısmı 
*user kayıt kısmına bak
*       UCMAK İSTERSEN 
*           sağ click olayı 
*           rename olayı 
*           bana anımsat olayı 
*
 */


namespace to_do
{
    public partial class Form1 : Form
    {
//        public DataGridView dataGridView1;

        string columnname;//secilen kolon name si
        public int userid = 2;
        private int rowCount;
        List<string[]> newRows = new List<string[]>();
        DateTime today = DateTime.Today;
        public Form1()
        {
            
            InitializeComponent();
            PrintTasks();
            rowCount = dataGridView1.Rows.Count;//hepsini sayar
            {
                dataGridView3.RowHeadersVisible = true;//3. grid
                dataGridView3.ColumnHeadersVisible = false;
                // dataGridView1.DefaultCellStyle.BackColor = Color.Transparent;
                dataGridView3.Columns.Clear();
                // Satır başlıklarını temizler
                dataGridView3.Rows.Clear();
                // Tek sütun ekler
                dataGridView3.Columns.Add("Column1", "Column1");
                // Satırları oluşturur
                // dataGridView3.Rows.Add();
                //dataGridView3.ColumnCount = 0;
                // Satır başlıklarına değerler atar
                dataGridView3.Rows.Add("All");
                dataGridView3.Rows.Add("pending");
                dataGridView3.Rows.Add("complete");
                dataGridView3.Rows.Add("manage category");

            }//3.grid
            List<string> category_names = TakeCategory();
            foreach (string headerText in category_names)
            {
                DataGridViewLinkColumn column = new DataGridViewLinkColumn();
                column.HeaderText = headerText;
                column.Name = headerText;
                dataGridView2.Columns.Add(column);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("tasks name", $"{columnname}", $"{today.ToString("dd/MM/yyyy")}",    $"{today.ToString("dd/MM/yyyy")}", false);
            savebtn.Show();
        }//add butonu 
        private void dataGridView2_CellContentClick(object sender,
            DataGridViewCellEventArgs e)
        {
            //dataGridView1.Rows.Clear();
            if (e.ColumnIndex == 0) // 'all' sütununa tıklandığında sayfa yanılenıyor ve addbtn gizleniyor
            {
                addbtn.Hide();
              //  dataGridView1.Refresh();
                columnname = null;
                PrintTasks();

            }
            else
            {
                addbtn.Show();
                //dataGridView1.Refresh();
                columnname = dataGridView2.Columns[e.ColumnIndex].Name;
                PrintTasks($"category_name = '{columnname}'");
            }
           
                rowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);
                Console.WriteLine(rowCount);
            Console.WriteLine($"Seçilen sütun: {columnname}");
  
        }//all butonuna basıldında olcaklar
        private void savebtn_Click(object sender, EventArgs e)
        {

            Console.WriteLine("basıldı");
            MySqlConnection connect = Connect();
          
            int totalRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Visible);//sadece görunen rows sayar 
//           int newRowCount = totalRowCount - rowCount;
            for (int i = rowCount; i < totalRowCount; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                string[] values = new string[row.Cells.Count];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    values[j] = row.Cells[j].Value.ToString();
                }
                newRows.Add(values);
            }//yeni eklenen verileri newRows adında diziye kayıt ettik 
            Console.WriteLine("kayıt edilde");
            // Yeni eklenen satırları veritabanına kaydetmek için bir döngü oluşturun
            foreach (string[] values in newRows)                    //2 tane eklediğinde calısıyormu acaba
            {
                // Burada verileri veritabanına kaydedebilirsiniz
                string des = values[0];
                string cat = values[1];
                string start = values[2];
                string end = values[3];
                string status = values[4];
                Console.WriteLine("kayıtlar basladı ");
                Save(connect, des, cat, start, end, status);

            }
            rowCount = dataGridView1.Rows.Count;//hepsini sayar
           newRows.Clear();



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
        public void Save(MySqlConnection conn, string title, string category, string
            startDate, string endDate, string status)
        {
            Dictionary<string,int> myDictionary = GetCategories();
            bool statusBool = bool.Parse(status);
            startDate = startDate.Replace("/", "-");
            endDate = endDate.Replace("/", "-");
            string[] startDates = startDate.Split('-');
            string[] endDates = endDate.Split('-');
            endDate = endDates[2] + "-" + endDates[1] + "-" + endDates[0];
            startDate = startDates[2] + "-" + startDates[1] + "-" + startDates[0];


            Console.WriteLine("save fok ");
            string sql = "INSERT INTO tasks (userid, categoryid, title,startdate,enddate,status) " +
                $"VALUES ({userid},{myDictionary[category]},'{title}','{startDate}'," + $"'{endDate}',{statusBool})";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int rowsAffected = cmd.ExecuteNonQuery();
            //Console.Clear();
            Console.WriteLine($"{rowsAffected} kayıt eklendi.");
            //%Y-%m-%d


        }


        public Dictionary<string,int> GetCategories()
        {
            string sql;
            Dictionary<string, int> myDictionary = new Dictionary<string, int>();
            MySqlConnection conn;
            conn = Connect();
            //   sql = $"SELECT tasks.id,categoryid,title ,startdate ,enddate ,status,category.id ," +
            //     $"category.category_name  FROM tasks INNER JOIN category ON tasks.categoryId = category.id WHERE tasks.userid={userid}";
            sql = $"SELECT * FROM category where userid={userid}";
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Verileri oku ve kullani
                        string categoryname = reader.GetString("category_name");
                        int categoryid=reader.GetInt32("id");
                        if (!myDictionary.ContainsKey(categoryname))
                            myDictionary.Add(categoryname, categoryid);
                        else continue;
                       
                    }
                }
            }


            return myDictionary;
        }

        public void PrintTasks(string filter = null)
        {
            dataGridView1.Rows.Clear();

            MySqlConnection conn;
            conn = Connect();
            string sql;

            if (filter != null)
            {
                sql = $"SELECT * FROM tasks INNER JOIN category ON tasks.categoryId = category.id WHERE tasks.userid={userid} and {filter}";
            }
            else
            {
                sql = $"SELECT * FROM tasks INNER JOIN category ON tasks.categoryId = category.id WHERE tasks.userid={userid}";
            }

            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Verileri oku ve kullani
                        object title = reader.GetString("title");
                        object categoryname = reader.GetString("category_name");
                        object startdate = reader.GetDateTime("startdate").ToShortDateString();
                        object enddate = reader.GetDateTime("enddate").ToShortDateString();
                        bool status = reader.GetBoolean("status");
                        dataGridView1.Refresh();
                        dataGridView1.Rows.Add($"{title}", $"{categoryname}", $"{startdate}", $"{enddate}", status);
                        //   dataGridView1.Refresh();
                    }
                }
            }
        }
        public List<string> TakeCategory()
        {
            List<string> stringList = new List<string>();

            MySqlConnection conn;
            conn = Connect();
            string sql = $"SELECT * FROM category where userid={userid}";
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Verileri oku ve kullani
                        object value = reader.GetString("category_name");
                        if (value != null && value != DBNull.Value)
                        {
                            string category_name = value.ToString();
                            stringList.Add(category_name);
                        }
                     
                    }
                }
            }
        return stringList;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0) PrintTasks();
            if(e.RowIndex == 1) PrintTasks("status=false");
            if (e.RowIndex == 2) PrintTasks("status=true");
            if (e.RowIndex == 3)
            {
                Form2 form2 = new Form2(this, dataGridView1);
                form2.ShowDialog();
                dataGridView1.Refresh();
            }
        }//all peding complate kısmı ve manage show kısmı 
        public void RefreshDataGridView()
        {
            // DataGridView1'in verilerini yenileme kodu buraya gelecek.
            // Örneğin:
            // dataGridView1.DataSource = null;
            dataGridView2.Columns.Clear();
            List<string> category_names = TakeCategory();

            DataGridViewLinkColumn All = new DataGridViewLinkColumn();
            All.Name = "All";
            dataGridView2.Columns.Add(All);
            foreach (string headerText in category_names)
            {
                DataGridViewLinkColumn column = new DataGridViewLinkColumn();
                column.HeaderText = headerText;
                column.Name = headerText;
                dataGridView2.Columns.Add(column);
            }
            PrintTasks();
            dataGridView2.Refresh();
        }
    }



    
}
    
