using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Client
{

    public partial class Main : Form
    {

        Socket sck;
        private string clientName = "";
        private bool nameSent = false;  // Flag to check if name is sent

        private CancellationTokenSource cancellationTokenSource;


        public Main()
        {
            InitializeComponent();

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            listFiles.SelectedIndexChanged += ListView_ItemActivate;

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void SendClick(object sender, EventArgs e)
        {
            int s = sck.Send(Encoding.Default.GetBytes(txtMessage.Text));

            if (s > 0)
            {
                MessageBox.Show("Data Sent !");
            }
        }


        private void ConnectionClick(object sender, EventArgs e)//connection button
        {
            string ip = txtIP.Text;       // Get the IP address from the textbox
            string portText = txtPort.Text;  // Get the port text from the textbox

            // Validate and parse the port
            if (int.TryParse(portText, out int port))
            {
                // kushti qe na siguron se porti eshte ne rangun (0-65535)
                if (port >= 0 && port <= 65535)
                {
                    // validojme ip adresen
                    if (IPAddress.TryParse(ip, out _))
                    {
                        try
                        {
                            // e lidhim socketin me ip dhe portin e dhene
                            sck.Connect(ip, port);

                            //dergimi i emrit te klientit ne fillim te lidhjes
                            if (!nameSent)
                            {
                                string name = txtName.Text;
                                byte[] nameBytes = Encoding.Default.GetBytes(name);
                                sck.Send(nameBytes);
                                nameSent = true;  // per te treguar se emri eshte vendosur
                                clientName = name;
                                client_Received(); // thirrja e metodes qe e krijon threadin per klientin

                                MessageBox.Show("Connected and name sent!");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Ne rast te deshtimit te lidhjes
                            MessageBox.Show($"Connection failed: {ex.Message}");
                        }
                    }
                    else
                    {
                        // Ne rast se IP nuk eshte valide
                        MessageBox.Show("Invalid IP address.");
                    }
                }
                else
                {
                    // Ne rast se porti nuk eshte valid
                    MessageBox.Show("Port number must be between 0 and 65535.");
                }
            }
            else
            {
                // port jo valid
                MessageBox.Show("Invalid port number.");
            }
        }

        //metoda qe krijon threadin e klientit dhe e pranon mesazhet nga serveri

        void client_Received()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            new Thread(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        byte[] buffer = new byte[8192]; // bufer per ruajtjen e te dhenave te pranuara

                        // Receive the first part: Check if the message is a file transfer request
                        int receivedBytes = sck.Receive(buffer);
                        if (receivedBytes > 0)
                        {
                            // Convert the byte array to a string
                            string receivedData = Encoding.Default.GetString(buffer, 0, receivedBytes);


                            if (receivedData.StartsWith("SERVER WARNING: "))
                            {
                                listOutput.Items.Add(receivedData);

                            }
                            else if (receivedData.StartsWith("~")) //kur nga serveri i dergojme filet qe jane shared me klienta
                            {
                                //emrat e fileve i ndajme me ~
                                string[] fileNames = receivedData.Split(new[] { '~' }, StringSplitOptions.RemoveEmptyEntries);

                                // e pastrojme kur e bejme update
                                listFiles.Items.Clear();
                                foreach (var fileName in fileNames)
                                {
                                    listFiles.Items.Add(fileName);
                                }
                            }
                            else if (receivedData == "RECEIVE FILE")
                            {

                                //kur pranojme file (nga kerkesa per read) e therrasim medoten qe e
                                //ben receive filen
                                ReceiveFile(txtSelectedFile.Text);
                                //pasi kryhet metoda per pranim te files i tregojme serverit se file eshte pranuar
                                sck.Send(Encoding.Default.GetBytes("File arrived to client " + clientName + "  succesfully at time: " + DateTime.Now));
                            }
                            else if (receivedData.StartsWith("EXECUTION RESULT: ")) //nese eshte ekzekutim
                            {

                                listOutput.Items.Add(receivedData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while communicating with server, please try again !");
                    }
                }
            }).Start();
        }

        //metoda qe merr emrin e files qe klikohet per read, write apo execute
        private void ListView_ItemActivate(object sender, EventArgs e)
        {
            string selectedItem = listFiles.SelectedItem.ToString();
            txtSelectedFile.Text = selectedItem;
        }
        void ReceiveFile(string fileName)
        {
            try
            {
                // Prepare a buffer to receive file data
                byte[] buffer = new byte[8192];
                int receivedBytes = 0;

                // First, receive the length of the file name
                receivedBytes = sck.Receive(buffer, 0, 4, SocketFlags.None);
                int fileNameLength = BitConverter.ToInt32(buffer, 0); // The length of the file name
                string fullFileName = Path.Combine(@txtSavingFilePath.Text, fileName); // Specify the path to save the file
                                                                                       // Receive the file name itself
                receivedBytes = sck.Receive(buffer, 0, fileNameLength, SocketFlags.None);
                string receivedFileName = Encoding.Default.GetString(buffer, 0, fileNameLength);

                // Receive the file size
                receivedBytes = sck.Receive(buffer, 0, 8, SocketFlags.None);
                long fileSize = BitConverter.ToInt64(buffer, 0); // The size of the file to be received
                
                // Create a file stream to write the incoming data to disk
                using (FileStream fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write))
                {
                    long bytesReceived = 0;
                    while (bytesReceived < fileSize)
                    {
                        // Calculate how many bytes to read in this iteration
                        int bytesToRead = (int)Math.Min(buffer.Length, fileSize - bytesReceived);

                        // Receive data into the buffer
                        receivedBytes = sck.Receive(buffer, 0, bytesToRead, SocketFlags.None);
                        // Write the received bytes to the file
                        fs.Write(buffer, 0, receivedBytes);
                        bytesReceived += receivedBytes;


                    }
                }

                MessageBox.Show($"File {receivedFileName} received and saved to {fullFileName}");
                listOutput.Items.Add("File received successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error receiving the file: {ex.Message}");
                listOutput.Items.Add("Error while receiving file");
            }
        }





        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CloseClick(object sender, EventArgs e)
        {
            //kerkesa per ndalimin e threadit
            cancellationTokenSource?.Cancel();

            //mbylle formen the liroj resurset e socketit
            sck.Close();
            sck.Dispose();

            //pret deri sa threadi ta perfundoje punen
            cancellationTokenSource?.Dispose();

            //mbylle formen
            Close();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //krijimi dhe dergimi i kerkeses per read
        private void ReadClick(object sender, EventArgs e)
        {
            string request = "~read~";
            request += txtSelectedFile.Text;
            sck.Send(Encoding.Default.GetBytes(request));


        }
        //krijimi dhe dergimi i kerkeses per write
        private void WriteClick(object sender, EventArgs e)
        {
            string request = "~writ~";
            request += txtSelectedFile.Text;
            request += "~";
            request += txtMessage.Text;
            request += " ";
            sck.Send(Encoding.Default.GetBytes(request));
        }
        //krijimi dhe dergimi i kerkeses per execute
        private void ExecuteClick(object sender, EventArgs e)
        {
            string request = "~exec~";
            request += txtSelectedFile.Text;
            sck.Send(Encoding.Default.GetBytes(request));
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
       


    }









}
