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
    public partial class Form2 : Form
    {
        private System.Windows.Forms.MonthCalendar monthCalendar1;

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DateTimePicker dateTimePicker1 = new DateTimePicker();
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "MMMM dd, yyyy - dddd";
            //dateTimePicker1.ShowUpDown = true;
            //dateTimePicker1.Visible = true;
            //dateTimePicker1.Location = new Point(10, 10);
            //dateTimePicker1.Size = new Size(200, 20);
            //this.Controls.Add(dateTimePicker1);
            //dateTimePicker1.BringToFront();


            //MonthCalendar monthCalendar1 = new MonthCalendar();
            //monthCalendar1.Location = new Point(10, 10);
            //monthCalendar1.ShowTodayCircle = false;
            //this.Controls.Add(monthCalendar1);
            //monthCalendar1.Size = new Size(200, 200);
            //monthCalendar1.BringToFront();
            Form3 form3 = new Form3();
            form3.Show();


        }
    }
}
