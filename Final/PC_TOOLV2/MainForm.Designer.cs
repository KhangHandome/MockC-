namespace PC_TOOLV2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.volang_picturebox = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusConnectBtn = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.SettingBTN = new System.Windows.Forms.Button();
            this.AdvanteSettingBtn = new System.Windows.Forms.Button();
            this.RotationWarningBtn = new System.Windows.Forms.Button();
            this.Forwader = new System.IO.Ports.SerialPort(this.components);
            this.DistanceWarningBtn = new System.Windows.Forms.Button();
            this.pingLabel = new System.Windows.Forms.Label();
            this.rotationWarningLabel = new System.Windows.Forms.Label();
            this.distanceWarningLabel = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.rotaionLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.debug1 = new System.Windows.Forms.TextBox();
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
            // AdvanteSettingBtn
            // 
            this.AdvanteSettingBtn.Image = ((System.Drawing.Image)(resources.GetObject("AdvanteSettingBtn.Image")));
            this.AdvanteSettingBtn.Location = new System.Drawing.Point(940, 248);
            this.AdvanteSettingBtn.Name = "AdvanteSettingBtn";
            this.AdvanteSettingBtn.Size = new System.Drawing.Size(100, 100);
            this.AdvanteSettingBtn.TabIndex = 9;
            this.AdvanteSettingBtn.UseVisualStyleBackColor = true;
            this.AdvanteSettingBtn.Click += new System.EventHandler(this.button2_Click);
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
            // Forwader
            // 
            this.Forwader.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Forwader_DataReceived);
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
            // pingLabel
            // 
            this.pingLabel.Location = new System.Drawing.Point(1012, 9);
            this.pingLabel.Name = "pingLabel";
            this.pingLabel.Size = new System.Drawing.Size(60, 15);
            this.pingLabel.TabIndex = 21;
            this.pingLabel.Text = "Ping";
            // 
            // rotationWarningLabel
            // 
            this.rotationWarningLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotationWarningLabel.Location = new System.Drawing.Point(153, 520);
            this.rotationWarningLabel.Name = "rotationWarningLabel";
            this.rotationWarningLabel.Size = new System.Drawing.Size(180, 25);
            this.rotationWarningLabel.TabIndex = 22;
            this.rotationWarningLabel.Text = "Rotation";
            this.rotationWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // distanceWarningLabel
            // 
            this.distanceWarningLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceWarningLabel.Location = new System.Drawing.Point(645, 520);
            this.distanceWarningLabel.Name = "distanceWarningLabel";
            this.distanceWarningLabel.Size = new System.Drawing.Size(180, 25);
            this.distanceWarningLabel.TabIndex = 23;
            this.distanceWarningLabel.Text = "Distance";
            this.distanceWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // distanceLabel
            // 
            this.distanceLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceLabel.Location = new System.Drawing.Point(608, 89);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(228, 25);
            this.distanceLabel.TabIndex = 25;
            this.distanceLabel.Text = "Connection";
            this.distanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rotaionLabel
            // 
            this.rotaionLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rotaionLabel.Location = new System.Drawing.Point(114, 90);
            this.rotaionLabel.Name = "rotaionLabel";
            this.rotaionLabel.Size = new System.Drawing.Size(228, 25);
            this.rotaionLabel.TabIndex = 24;
            this.rotaionLabel.Text = "Connection";
            this.rotaionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // debug1
            // 
            this.debug1.Location = new System.Drawing.Point(182, 29);
            this.debug1.Name = "debug1";
            this.debug1.Size = new System.Drawing.Size(228, 20);
            this.debug1.TabIndex = 26;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 590);
            this.Controls.Add(this.debug1);
            this.Controls.Add(this.distanceLabel);
            this.Controls.Add(this.rotaionLabel);
            this.Controls.Add(this.distanceWarningLabel);
            this.Controls.Add(this.rotationWarningLabel);
            this.Controls.Add(this.pingLabel);
            this.Controls.Add(this.DistanceWarningBtn);
            this.Controls.Add(this.RotationWarningBtn);
            this.Controls.Add(this.AdvanteSettingBtn);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.SettingBTN);
            this.Controls.Add(this.statusConnectBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.volang_picturebox);
            this.Name = "MainForm";
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
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button SettingBTN;
        private System.Windows.Forms.Button AdvanteSettingBtn;
        private System.Windows.Forms.Button RotationWarningBtn;
        private System.IO.Ports.SerialPort Forwader;
        private System.Windows.Forms.Button DistanceWarningBtn;
        private System.Windows.Forms.Label pingLabel;
        private System.Windows.Forms.Label rotationWarningLabel;
        private System.Windows.Forms.Label distanceWarningLabel;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label rotaionLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox debug1;
    }
}

