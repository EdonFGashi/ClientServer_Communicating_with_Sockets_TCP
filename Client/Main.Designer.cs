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

            #endregion
        }
    }
}