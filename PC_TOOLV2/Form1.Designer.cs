namespace PC_TOOLV2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.volang_picturebox = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusConnectBtn = new System.Windows.Forms.Button();
            this.rotationTB = new System.Windows.Forms.TextBox();
            this.distanceTB = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.SettingBTN = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusNode1Btn = new System.Windows.Forms.Button();
            this.statusNode2Btn = new System.Windows.Forms.Button();
            this.distanceWariningTB = new System.Windows.Forms.TextBox();
            this.RotationWarningBtn = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.portnameLable = new System.Windows.Forms.Label();
            this.rotationWarningTb = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.DistanceWarningBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.pingLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.volang_picturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // volang_picturebox
            // 
            this.volang_picturebox.Image = ((System.Drawing.Image)(resources.GetObject("volang_picturebox.Image")));
            this.volang_picturebox.InitialImage = null;
            this.volang_picturebox.Location = new System.Drawing.Point(42, 118);
            this.volang_picturebox.Name = "volang_picturebox";
            this.volang_picturebox.Size = new System.Drawing.Size(400, 400);
            this.volang_picturebox.TabIndex = 0;
            this.volang_picturebox.TabStop = false;
            this.volang_picturebox.UseWaitCursor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(524, 117);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connection";
            // 
            // statusConnectBtn
            // 
            this.statusConnectBtn.BackColor = System.Drawing.Color.Lime;
            this.statusConnectBtn.Location = new System.Drawing.Point(137, 26);
            this.statusConnectBtn.Name = "statusConnectBtn";
            this.statusConnectBtn.Size = new System.Drawing.Size(28, 23);
            this.statusConnectBtn.TabIndex = 3;
            this.statusConnectBtn.UseVisualStyleBackColor = false;
            // 
            // rotationTB
            // 
            this.rotationTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotationTB.Location = new System.Drawing.Point(182, 524);
            this.rotationTB.Name = "rotationTB";
            this.rotationTB.Size = new System.Drawing.Size(100, 38);
            this.rotationTB.TabIndex = 5;
            this.rotationTB.Text = "0°";
            this.rotationTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // distanceTB
            // 
            this.distanceTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceTB.Location = new System.Drawing.Point(660, 523);
            this.distanceTB.Name = "distanceTB";
            this.distanceTB.Size = new System.Drawing.Size(147, 38);
            this.distanceTB.TabIndex = 6;
            this.distanceTB.Text = "120cm";
            this.distanceTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(423, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(130, 70);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // SettingBTN
            // 
            this.SettingBTN.ForeColor = System.Drawing.Color.White;
            this.SettingBTN.Image = ((System.Drawing.Image)(resources.GetObject("SettingBTN.Image")));
            this.SettingBTN.Location = new System.Drawing.Point(940, 116);
            this.SettingBTN.Name = "SettingBTN";
            this.SettingBTN.Size = new System.Drawing.Size(100, 100);
            this.SettingBTN.TabIndex = 7;
            this.SettingBTN.UseVisualStyleBackColor = true;
            this.SettingBTN.Click += new System.EventHandler(this.SettingBTN_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(940, 248);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 100);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusNode1Btn
            // 
            this.statusNode1Btn.BackColor = System.Drawing.Color.Lime;
            this.statusNode1Btn.Location = new System.Drawing.Point(42, 118);
            this.statusNode1Btn.Name = "statusNode1Btn";
            this.statusNode1Btn.Size = new System.Drawing.Size(28, 23);
            this.statusNode1Btn.TabIndex = 10;
            this.statusNode1Btn.UseVisualStyleBackColor = false;
            // 
            // statusNode2Btn
            // 
            this.statusNode2Btn.BackColor = System.Drawing.Color.Lime;
            this.statusNode2Btn.Location = new System.Drawing.Point(524, 118);
            this.statusNode2Btn.Name = "statusNode2Btn";
            this.statusNode2Btn.Size = new System.Drawing.Size(28, 23);
            this.statusNode2Btn.TabIndex = 11;
            this.statusNode2Btn.UseVisualStyleBackColor = false;
            // 
            // distanceWariningTB
            // 
            this.distanceWariningTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceWariningTB.Location = new System.Drawing.Point(620, 74);
            this.distanceWariningTB.Name = "distanceWariningTB";
            this.distanceWariningTB.Size = new System.Drawing.Size(203, 38);
            this.distanceWariningTB.TabIndex = 12;
            this.distanceWariningTB.Text = "Safety : 120cm";
            this.distanceWariningTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RotationWarningBtn
            // 
            this.RotationWarningBtn.BackColor = System.Drawing.Color.Red;
            this.RotationWarningBtn.Location = new System.Drawing.Point(182, 494);
            this.RotationWarningBtn.Name = "RotationWarningBtn";
            this.RotationWarningBtn.Size = new System.Drawing.Size(107, 23);
            this.RotationWarningBtn.TabIndex = 15;
            this.RotationWarningBtn.UseVisualStyleBackColor = false;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // portnameLable
            // 
            this.portnameLable.AutoSize = true;
            this.portnameLable.Location = new System.Drawing.Point(5, 9);
            this.portnameLable.Name = "portnameLable";
            this.portnameLable.Size = new System.Drawing.Size(57, 13);
            this.portnameLable.TabIndex = 16;
            this.portnameLable.Text = "Port Name";
            // 
            // rotationWarningTb
            // 
            this.rotationWarningTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotationWarningTb.Location = new System.Drawing.Point(154, 74);
            this.rotationWarningTb.Name = "rotationWarningTb";
            this.rotationWarningTb.Size = new System.Drawing.Size(203, 38);
            this.rotationWarningTb.TabIndex = 17;
            this.rotationWarningTb.Text = "Safety : ±30°";
            this.rotationWarningTb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // DistanceWarningBtn
            // 
            this.DistanceWarningBtn.BackColor = System.Drawing.Color.Red;
            this.DistanceWarningBtn.Location = new System.Drawing.Point(676, 494);
            this.DistanceWarningBtn.Name = "DistanceWarningBtn";
            this.DistanceWarningBtn.Size = new System.Drawing.Size(107, 23);
            this.DistanceWarningBtn.TabIndex = 18;
            this.DistanceWarningBtn.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 62);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(140, 20);
            this.textBox2.TabIndex = 19;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(189, 29);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(228, 20);
            this.textBox3.TabIndex = 20;
            // 
            // pingLabel
            // 
            this.pingLabel.AutoSize = true;
            this.pingLabel.Location = new System.Drawing.Point(983, 9);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(28, 13);
            this.pingLabel.TabIndex = 21;
            this.pingLabel.Text = "Ping";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 590);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.DistanceWarningBtn);
            this.Controls.Add(this.rotationWarningTb);
            this.Controls.Add(this.portnameLable);
            this.Controls.Add(this.RotationWarningBtn);
            this.Controls.Add(this.distanceWariningTB);
            this.Controls.Add(this.statusNode2Btn);
            this.Controls.Add(this.statusNode1Btn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.SettingBTN);
            this.Controls.Add(this.distanceTB);
            this.Controls.Add(this.rotationTB);
            this.Controls.Add(this.statusConnectBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.volang_picturebox);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.volang_picturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox volang_picturebox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button statusConnectBtn;
        private System.Windows.Forms.TextBox rotationTB;
        private System.Windows.Forms.TextBox distanceTB;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button SettingBTN;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button statusNode1Btn;
        private System.Windows.Forms.Button statusNode2Btn;
        private System.Windows.Forms.TextBox distanceWariningTB;
        private System.Windows.Forms.Button RotationWarningBtn;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label portnameLable;
        private System.Windows.Forms.TextBox rotationWarningTb;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button DistanceWarningBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label pingLabel;
    }
}

