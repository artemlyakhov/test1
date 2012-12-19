namespace MConverter
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.y1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.x1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browse1 = new System.Windows.Forms.Button();
            this.scale1 = new System.Windows.Forms.TextBox();
            this.convert1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.mask1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.y1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.x1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.browse1);
            this.groupBox1.Controls.Add(this.scale1);
            this.groupBox1.Controls.Add(this.convert1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mask1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bmp->Png";
            // 
            // y1
            // 
            this.y1.Location = new System.Drawing.Point(108, 62);
            this.y1.Name = "y1";
            this.y1.Size = new System.Drawing.Size(33, 20);
            this.y1.TabIndex = 1;
            this.y1.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "X";
            // 
            // x1
            // 
            this.x1.Location = new System.Drawing.Point(48, 62);
            this.x1.Name = "x1";
            this.x1.Size = new System.Drawing.Size(34, 20);
            this.x1.TabIndex = 0;
            this.x1.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Frames:";
            // 
            // browse1
            // 
            this.browse1.Location = new System.Drawing.Point(377, 19);
            this.browse1.Name = "browse1";
            this.browse1.Size = new System.Drawing.Size(25, 20);
            this.browse1.TabIndex = 7;
            this.browse1.Text = "...";
            this.browse1.UseVisualStyleBackColor = true;
            // 
            // scale1
            // 
            this.scale1.Enabled = false;
            this.scale1.Location = new System.Drawing.Point(280, 62);
            this.scale1.Name = "scale1";
            this.scale1.Size = new System.Drawing.Size(91, 20);
            this.scale1.TabIndex = 3;
            this.scale1.Text = "1,0";
            // 
            // convert1
            // 
            this.convert1.Location = new System.Drawing.Point(48, 99);
            this.convert1.Name = "convert1";
            this.convert1.Size = new System.Drawing.Size(75, 23);
            this.convert1.TabIndex = 4;
            this.convert1.Text = "Convert";
            this.convert1.UseVisualStyleBackColor = true;
            this.convert1.Click += new System.EventHandler(this.convert1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scale:";
            // 
            // mask1
            // 
            this.mask1.Location = new System.Drawing.Point(48, 19);
            this.mask1.Name = "mask1";
            this.mask1.Size = new System.Drawing.Size(323, 20);
            this.mask1.TabIndex = 6;
            this.mask1.Text = "c:\\Users\\ArtemL\\Documents\\Moon\\Graphics3d\\x*.bmp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mask:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 195);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mask1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button convert1;
        private System.Windows.Forms.TextBox scale1;
        private System.Windows.Forms.TextBox x1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browse1;
        private System.Windows.Forms.TextBox y1;
        private System.Windows.Forms.Label label4;
    }
}

