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
    }









}
