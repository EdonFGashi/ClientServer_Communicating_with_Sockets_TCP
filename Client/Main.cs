using System.Net.Sockets;

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
                       

                }









    }
