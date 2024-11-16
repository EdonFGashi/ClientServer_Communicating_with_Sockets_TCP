using System.Net.Sockets;
using System.Text;

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

        private void button1_Click(object sender, EventArgs e)
        {
            int s = sck.Send(Encoding.Default.GetBytes(txtMessage.Text));

            if (s > 0)
            {
                MessageBox.Show("Data Sent !");
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


                    }









    }
