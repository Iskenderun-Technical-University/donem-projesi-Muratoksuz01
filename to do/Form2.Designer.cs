namespace to_do
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addcatebtn = new System.Windows.Forms.Button();
            this.delbtn = new System.Windows.Forms.Button();
            this.savebtn1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(-4, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(321, 200);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column Name";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 275;
            // 
            // addcatebtn
            // 
            this.addcatebtn.Location = new System.Drawing.Point(137, 12);
            this.addcatebtn.Name = "addcatebtn";
            this.addcatebtn.Size = new System.Drawing.Size(75, 23);
            this.addcatebtn.TabIndex = 1;
            this.addcatebtn.Text = "Add";
            this.addcatebtn.UseVisualStyleBackColor = true;
            this.addcatebtn.Click += new System.EventHandler(this.addcatebtn_Click);
            // 
            // delbtn
            // 
            this.delbtn.Location = new System.Drawing.Point(218, 12);
            this.delbtn.Name = "delbtn";
            this.delbtn.Size = new System.Drawing.Size(75, 23);
            this.delbtn.TabIndex = 2;
            this.delbtn.Text = "Delete";
            this.delbtn.UseVisualStyleBackColor = true;
            this.delbtn.Click += new System.EventHandler(this.delbtn_Click);
            // 
            // savebtn1
            // 
            this.savebtn1.Location = new System.Drawing.Point(206, 253);
            this.savebtn1.Name = "savebtn1";
            this.savebtn1.Size = new System.Drawing.Size(75, 23);
            this.savebtn1.TabIndex = 3;
            this.savebtn1.Text = "Save";
            this.savebtn1.UseVisualStyleBackColor = true;
            this.savebtn1.Click += new System.EventHandler(this.savebtn1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 285);
            this.Controls.Add(this.savebtn1);
            this.Controls.Add(this.delbtn);
            this.Controls.Add(this.addcatebtn);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "manage category";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button addcatebtn;
        private System.Windows.Forms.Button delbtn;
        private System.Windows.Forms.Button savebtn1;
    }
}