namespace DrawingProgram {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.goButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.59018F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.40982F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.goButton, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.55798F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.44201F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 394);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 20);
            this.textBox1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(289, 384);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(298, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(636, 384);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(298, 394);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 3;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Drawing App";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button goButton;
    }
}

