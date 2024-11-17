using System.Xml.Linq;

namespace Client
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            btnSend = new Button();
            btnConnect = new Button();
            txtMessage = new TextBox();
            btnClose = new Button();
            txtIP = new TextBox();
            txtPort = new TextBox();
            txtName = new TextBox();
            listFiles = new ListBox();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            listOutput = new ListBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtSelectedFile = new TextBox();
            label7 = new Label();
            txtSavingFilePath = new TextBox();
            label8 = new Label();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.BackColor = SystemColors.GrayText;
            btnSend.Location = new Point(190, 237);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(160, 34);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += button1_Click;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = SystemColors.Info;
            btnConnect.Location = new Point(190, 113);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(160, 31);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += button1_Click_1;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(62, 204);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(288, 27);
            txtMessage.TabIndex = 2;
            txtMessage.TextChanged += textBox1_TextChanged;
            // 
            // btnClose
            // 
            btnClose.BackColor = SystemColors.ActiveCaption;
            btnClose.Location = new Point(578, 473);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(249, 42);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(46, 47);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(165, 27);
            txtIP.TabIndex = 4;
            // 
            // txtPort
            // 
            txtPort.Location = new Point(272, 47);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(78, 27);
            txtPort.TabIndex = 5;
            // 
            // txtName
            // 
            txtName.Location = new Point(74, 80);
            txtName.Name = "txtName";
            txtName.Size = new Size(276, 27);
            txtName.TabIndex = 6;
            // 
            // listFiles
            // 
            listFiles.FormattingEnabled = true;
            listFiles.Location = new Point(389, 47);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(441, 144);
            listFiles.TabIndex = 7;
            listFiles.SelectedIndexChanged += listFiles_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(389, 24);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 8;
            label1.Text = "Server Files";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(407, 240);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 9;
            button1.Text = "Read";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_2;
            // 
            // button2
            // 
            button2.Location = new Point(561, 240);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 10;
            button2.Text = "Write";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(733, 240);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 11;
            button3.Text = "Execute";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // listOutput
            // 
            listOutput.FormattingEnabled = true;
            listOutput.Location = new Point(12, 343);
            listOutput.Name = "listOutput";
            listOutput.Size = new Size(815, 124);
            listOutput.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 50);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 13;
            label2.Text = "IP:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(228, 51);
            label3.Name = "label3";
            label3.Size = new Size(38, 20);
            label3.TabIndex = 14;
            label3.Text = "Port:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 83);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 15;
            label4.Text = "Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 207);
            label5.Name = "label5";
            label5.Size = new Size(44, 20);
            label5.TabIndex = 16;
            label5.Text = "Data:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 320);
            label6.Name = "label6";
            label6.Size = new Size(58, 20);
            label6.TabIndex = 17;
            label6.Text = "Output:";
            // 
            // txtSelectedFile
            // 
            txtSelectedFile.Location = new Point(489, 204);
            txtSelectedFile.Name = "txtSelectedFile";
            txtSelectedFile.Size = new Size(338, 27);
            txtSelectedFile.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(389, 207);
            label7.Name = "label7";
            label7.Size = new Size(94, 20);
            label7.TabIndex = 19;
            label7.Text = "Selected file:";
            // 
            // txtSavingFilePath
            // 
            txtSavingFilePath.Location = new Point(389, 295);
            txtSavingFilePath.Name = "txtSavingFilePath";
            txtSavingFilePath.Size = new Size(441, 27);
            txtSavingFilePath.TabIndex = 20;
            txtSavingFilePath.Text = "C:\\Users\\Dell\\Desktop\\ClientFiles";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(268, 298);
            label8.Name = "label8";
            label8.Size = new Size(115, 20);
            label8.TabIndex = 21;
            label8.Text = "File saving path:";
            label8.Click += label8_Click;

        }
    }
}