namespace to_do
{
    partial class Form3
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
            this.btnkayit = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lbluser = new System.Windows.Forms.Label();
            this.lbemail = new System.Windows.Forms.Label();
            this.lbpass = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbgiris = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnkayit
            // 
            this.btnkayit.Location = new System.Drawing.Point(385, 261);
            this.btnkayit.Name = "btnkayit";
            this.btnkayit.Size = new System.Drawing.Size(92, 23);
            this.btnkayit.TabIndex = 0;
            this.btnkayit.Text = "KAYIT OL";
            this.btnkayit.UseVisualStyleBackColor = true;
            this.btnkayit.Click += new System.EventHandler(this.btnkayit_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(399, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(399, 137);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(399, 190);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(92, 22);
            this.textBox3.TabIndex = 4;
            // 
            // lbluser
            // 
            this.lbluser.AutoSize = true;
            this.lbluser.Location = new System.Drawing.Point(261, 77);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(84, 16);
            this.lbluser.TabIndex = 5;
            this.lbluser.Text = "USERNAME";
            // 
            // lbemail
            // 
            this.lbemail.AutoSize = true;
            this.lbemail.Location = new System.Drawing.Point(261, 143);
            this.lbemail.Name = "lbemail";
            this.lbemail.Size = new System.Drawing.Size(46, 16);
            this.lbemail.TabIndex = 6;
            this.lbemail.Text = "EMAIL";
            // 
            // lbpass
            // 
            this.lbpass.AutoSize = true;
            this.lbpass.Location = new System.Drawing.Point(261, 196);
            this.lbpass.Name = "lbpass";
            this.lbpass.Size = new System.Drawing.Size(86, 16);
            this.lbpass.TabIndex = 7;
            this.lbpass.Text = "PASSWORD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "do you have an";
            // 
            // lbgiris
            // 
            this.lbgiris.AutoSize = true;
            this.lbgiris.ForeColor = System.Drawing.Color.Red;
            this.lbgiris.Location = new System.Drawing.Point(379, 233);
            this.lbgiris.Name = "lbgiris";
            this.lbgiris.Size = new System.Drawing.Size(54, 16);
            this.lbgiris.TabIndex = 9;
            this.lbgiris.Text = "account";
            this.lbgiris.Click += new System.EventHandler(this.lbgiris_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbgiris);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbpass);
            this.Controls.Add(this.lbemail);
            this.Controls.Add(this.lbluser);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnkayit);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnkayit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.Label lbemail;
        private System.Windows.Forms.Label lbpass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbgiris;
    }
}