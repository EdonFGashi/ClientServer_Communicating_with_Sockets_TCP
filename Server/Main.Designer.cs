namespace Server
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            listClients = new ListView();
            colEndpoint = new ColumnHeader();
            colID = new ColumnHeader();
            colMessage = new ColumnHeader();
            colTime = new ColumnHeader();
            colName = new ColumnHeader();
            txtIP = new TextBox();
            txtPort = new TextBox();
            btnListen = new Button();
            ts = new TabControl();
            tabClients = new TabPage();
            txtSharedFolderPath = new TextBox();
            label4 = new Label();
            picListening = new PictureBox();
            btnShowIP = new Button();
            label2 = new Label();
            label1 = new Label();
            tabChat = new TabPage();
            listBox1 = new ListBox();
            txtClient = new TextBox();
            checkRead = new CheckBox();
            checkWrite = new CheckBox();
            checkExecute = new CheckBox();
            label3 = new Label();
            button1 = new Button();
            btnUpdateFileAccess = new Button();
            txtFiles = new TextBox();
            ts.SuspendLayout();
            tabClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picListening).BeginInit();
            tabChat.SuspendLayout();
            SuspendLayout();
            // 
            // listClients
            // 
            listClients.Columns.AddRange(new ColumnHeader[] { colEndpoint, colID, colMessage, colTime, colName });
            listClients.Location = new Point(18, 107);
            listClients.Name = "listClients";
            listClients.Size = new Size(822, 197);
            listClients.TabIndex = 0;
            listClients.UseCompatibleStateImageBehavior = false;
            listClients.View = View.Details;
            //listClients.SelectedIndexChanged += listClients_SelectedIndexChanged;
            // 
            // colEndpoint
            // 
            colEndpoint.Text = "EndPoint";
            colEndpoint.Width = 100;
            // 
            // colID
            // 
            colID.Text = "ID";
            colID.Width = 240;
            // 
            // colMessage
            // 
            colMessage.DisplayIndex = 3;
            colMessage.Text = "Last Message";
            colMessage.Width = 200;
            // 
            // colTime
            // 
            colTime.DisplayIndex = 4;
            colTime.Text = "Last Rec Time";
            colTime.Width = 180;
            // 
            // colName
            // 
            colName.DisplayIndex = 2;
            colName.Text = "Name";
            colName.Width = 100;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(55, 21);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(193, 27);
            txtIP.TabIndex = 1;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(497, 20);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(94, 27);
            txtPort.TabIndex = 2;
            // 
            // btnListen
            // 
            btnListen.BackColor = Color.Lime;
            btnListen.Location = new Point(614, 20);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(166, 29);
            btnListen.TabIndex = 3;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = false;
            //btnListen.Click += button1_Click;
            // 
            // ts
            // 
            ts.Controls.Add(tabClients);
            ts.Controls.Add(tabChat);
            ts.Location = new Point(12, 12);
            ts.Name = "ts";
            ts.SelectedIndex = 0;
            ts.Size = new Size(870, 343);
            ts.TabIndex = 4;
            // 
            // tabClients
            // 
            tabClients.Controls.Add(txtSharedFolderPath);
            tabClients.Controls.Add(label4);
            tabClients.Controls.Add(picListening);
            tabClients.Controls.Add(btnShowIP);
            tabClients.Controls.Add(label2);
            tabClients.Controls.Add(label1);
            tabClients.Controls.Add(txtIP);
            tabClients.Controls.Add(listClients);
            tabClients.Controls.Add(btnListen);
            tabClients.Controls.Add(txtPort);
            tabClients.Location = new Point(4, 29);
            tabClients.Name = "tabClients";
            tabClients.Padding = new Padding(3);
            tabClients.Size = new Size(862, 310);
            tabClients.TabIndex = 0;
            tabClients.Text = "Clients";
            tabClients.UseVisualStyleBackColor = true;
            // 
            // txtSharedFolderPath
            // 
            txtSharedFolderPath.Location = new Point(170, 70);
            txtSharedFolderPath.Name = "txtSharedFolderPath";
            txtSharedFolderPath.Size = new Size(427, 27);
            txtSharedFolderPath.TabIndex = 9;
            txtSharedFolderPath.Text = "C:\\\\Users\\\\Dell\\\\Desktop\\\\ServerFiles\\\\";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 73);
            label4.Name = "label4";
            label4.Size = new Size(136, 20);
            label4.TabIndex = 8;
            label4.Text = "Shared folder path:";
            
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 462);
            Controls.Add(txtFiles);
            Controls.Add(btnUpdateFileAccess);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(checkExecute);
            Controls.Add(checkWrite);
            Controls.Add(checkRead);
            Controls.Add(txtClient);
            Controls.Add(ts);
            Name = "Main";
            Text = "Server";
            ts.ResumeLayout(false);
            tabClients.ResumeLayout(false);
            tabClients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picListening).EndInit();
            tabChat.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listClients;
        private ColumnHeader colEndpoint;
        private ColumnHeader colID;
        private ColumnHeader colMessage;
        private ColumnHeader colTime;
        private TextBox txtIP;
        private TextBox txtPort;
        private Button btnListen;
        private TabControl ts;
        private TabPage tabClients;
        private TabPage tabChat;
        private ListBox listBox1;
        private ColumnHeader colName;
        private Label label1;
        private Label label2;
        private Button btnShowIP;
        private PictureBox picListening;
        private TextBox txtClient;
        private CheckBox checkRead;
        private CheckBox checkWrite;
        private CheckBox checkExecute;
        private Label label3;
        private Button button1;
        private TextBox txtSharedFolderPath;
        private Label label4;
        private Button btnUpdateFileAccess;
        private TextBox txtFiles;
    }
}
