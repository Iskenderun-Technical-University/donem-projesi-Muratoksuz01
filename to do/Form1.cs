using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
/*
    YAPILACAKLAR
*gorevler status yada biri değiştiğinde oto kayıt olacak /*gorevler kayıt altında olacak save ye basınca gozden gecirecek degişikliği update yapacak
*takvim kısmı  bir hata var         tamam
*gorevler silme islemi              tamam
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
            MySqlConnection connect = Connect();

            Console.WriteLine(" yeni row icin   basıldı");

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
            Console.WriteLine("burada");
            if (filter != null)
            {
                sql = $"SELECT * FROM tasks INNER JOIN category ON tasks.categoryId = category.id WHERE tasks.userid={userid} and {filter}";
            }
            else
            {
                sql = $"SELECT * FROM tasks INNER JOIN category ON tasks.categoryId = category.id WHERE tasks.userid={userid}";
            }
            Console.WriteLine("burada1");

            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("burada2");


                    while (reader.Read())
                    {
                        // Verileri oku ve kullani
                        object title = reader.GetString("title");
                        object categoryname = reader.GetString("category_name");
                        object startdate = reader.GetDateTime("startdate").ToString("yyyy/MM/dd");
                        object enddate = reader.GetDateTime("enddate").ToString("yyyy/MM/dd");
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

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string selectedDate = monthCalendar1.SelectionStart.ToString("yyyy/MM/dd");
            PrintTasks($"startdate='{selectedDate}'");
        }//basılan tarih gorevlerini getiriyor

        private void delbtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            conn = Connect();
            try
            {
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex; // seçilen hücrenin satır indeksi
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex]; // seçilen satır
            string cellValue = selectedRow.Cells[0].Value.ToString(); // seçilen satırdaki ilk hücrenin değeri

            Console.WriteLine(cellValue);
            string sql = $"DELETE FROM tasks WHERE title='{cellValue}'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int rowsAffected = cmd.ExecuteNonQuery();
            //Console.Clear();
            Console.WriteLine($"{rowsAffected} kayıt silindi.");
            PrintTasks();
            }
            catch (Exception ex)
            {
               Console.WriteLine("hata var "+ex.Message ); 
            }

        }//gorev silme 

       /* //private void dataGridView1_KeyDown(object sender, DataGridViewCellEventArgs e, KeyEventArgs k)
        //{
        //    Console.WriteLine(" anlasdvdjsk");
        //    MySqlConnection conn;
        //    conn = Connect();
        //    // Hangi hücre tıklandı
        //    while (true)
        //    {
        //        if (k.KeyCode == Keys.Enter)
        //        {
        //            DataGridViewCell clickedCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

        //            // Hangi görevin değiştirildiğini bulmak için tıklanan hücrenin satırını alın
        //            DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

        //            // Değiştirilen görevin ID'sini bulmak için satırın Tag özelliğini kullanın
        //            int taskId = (int)clickedRow.Tag;

        //            // Hangi sütun değiştirildi?
        //            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

        //            // Yapılacak işlemler
        //            // ...
        //            if (columnName == "description")
        //            {
        //                // Yeni değeri al
        //                string newDescription = clickedCell.Value.ToString();
        //                string sql = $"update tasks set title='{newDescription}' where id=9";

        //                MySqlCommand cmd = new MySqlCommand(sql, conn);
        //                int rowsAffected = cmd.ExecuteNonQuery();
        //                Console.WriteLine($" kayıt etkilende: {rowsAffected}");

        //            }
        //        }
        //    }

        //}
       */
      
        
        
        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(" anlasdvdjsk");
            DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];
            int taskId = (int)clickedRow.Tag;
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();

            switch (columnName)
            {
                case "title":
                    cmd.CommandText = $"UPDATE tasks SET title='{newValue}' WHERE id='{taskId}'";
                    break;
                case "startdate":
                    cmd.CommandText = $"UPDATE tasks SET startdate='{newValue}' WHERE id='{taskId}'";
                    break;
                case "enddate":
                    cmd.CommandText = $"UPDATE tasks SET enddate='{newValue}' WHERE id='{taskId}'";
                    break;
                case "status":
                    cmd.CommandText = $"UPDATE tasks SET status='{newValue}' WHERE id='{taskId}'";
                    break;
            }

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($" kayıt etkilende: {rowsAffected}");

            conn.Close();
        }

      

    }









}
    

