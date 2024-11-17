using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Xml.Linq;
using System.Globalization;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;


namespace Server
{
    public partial class Main : Form
    {
        Listener listener;
        string sharedFolderPath;
        public Main()
        {
            InitializeComponent();

            listClients.FullRowSelect = true;
            listClients.ItemActivate += ListView_ItemActivate;
        }

        // Event handler when a row is clicked in the ListView
        private void ListView_ItemActivate(object sender, EventArgs e)
        {
            // Ensure a row is selected
            if (listClients.SelectedItems.Count > 0)
            {
                var selectedItem = listClients.SelectedItems[0];

                // Get the value from column 2 (index 1) and set it in the TextBox
                txtClient.Text = selectedItem.SubItems[1].Text.ToString();


                try
                {
                    //Invoke((MethodInvoker)delegate
                    //{
                    string sharedFolderPath = txtSharedFolderPath.Text;
                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == txtClient.Text)
                        {
                            if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = false;
                                checkExecute.Checked = false;
                            }
                            else if (!client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = false;
                                checkWrite.Checked = true;
                                checkExecute.Checked = false;
                            }
                            else if (!client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                                && !client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                                && client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute))
                            {
                                checkRead.Checked = false;
                                checkWrite.Checked = false;
                                checkExecute.Checked = true;
                            }
                            else if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.All))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = true;
                                checkExecute.Checked = true;
                            }
                            else if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.ReadWrite))
                            {
                                checkRead.Checked = true;
                                checkWrite.Checked = true;
                                checkExecute.Checked = false;
                            }

                            break;
                        }
                    }
                    //});
                }
                catch (StackOverflowException soe)
                {

                }
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            //listener.Start();

        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += new Client.ClientReceivedHandler(client_Received);
            client.Disconnected += new Client.ClientDisconnectedHandler(client_Disconnected);

            //krijimi i instances qe ruan permission per klientin qe u lidh
            ClientPermissions client1Permissions = new ClientPermissions(client.ID);
            client.setClientPermissions(client1Permissions);

            Invoke((MethodInvoker)delegate
            {
                ListViewItem i = new ListViewItem();
                i.Text = client.EndPoint.ToString();
                i.SubItems.Add(client.ID);
                i.SubItems.Add("XX");
                i.SubItems.Add("XX");
                i.SubItems.Add(client.clientName.ToString());
                i.Tag = client;
                listClients.Items.Add(i);
            });

        }

        void client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < listClients.Items.Count; i++)
                {
                    Client client = listClients.Items[i].Tag as Client;

                    if (client.ID == sender.ID)
                    {
                        listClients.Items.RemoveAt(i);
                        break;
                    }
                }

            });
        }

        void client_Received(Client sender, byte[] data)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    string clientRequest = Encoding.Default.GetString(data);
                    string clearRequest = "";
                    string filePathRequest = txtSharedFolderPath.Text;


                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == sender.ID)
                        {
                            listClients.Items[i].SubItems[2].Text = Encoding.Default.GetString(data);
                            listClients.Items[i].SubItems[3].Text = DateTime.Now.ToString();
                            listBox1.Items.Add(client.clientName + " -- " + Encoding.Default.GetString(data));







                            //nese karakteri i pare esht ~ atehere kemi te bejme me ndonje kerkese per read, write apo execute
                            if (clientRequest.StartsWith("~"))
                            {

                                if (clientRequest.Substring(1, 4) == "read") //kerkesa per read
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);
                                    //filePathRequest += clientRequest.Substring(6);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Read))
                                    {
                                        //// Client has read permission, send file to client
                                        ///




                                        //////////////////////////////////////
                                        
                                    }
                                    else // nese nuk ka permission atehere i kthejme nje mesazh
                                    {
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for reading file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                        client.sck.Send(Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to read this file."));
                                    }

                                }
                                else if (clientRequest.Substring(1, 4) == "writ") // kerkesa per write
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Write))
                                    {
                                        //// Client has read permission, send file to client
                                        //SendFileToClient(clientSocket, requestedFile);




                                        ///////////////////////////////////////
                                        
                                    }
                                    else
                                    {
                                        byte[] denyMessage = Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to write in this file.");
                                        client.sck.Send(denyMessage);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for writing file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                    }
                                }
                                else if (clientRequest.Substring(1, 4) == "exec") // kerkesa per execute
                                {
                                    listBox1.Items.Add(clientRequest + "  ======  client name: " + client.clientName);

                                    //kontrollojme nese klienti ka leje per kete kerkese
                                    if (client.Permissions.HasPermission(filePathRequest, FilePermissions.Execute))
                                    {
                                        //// Client has read permission, send file to client
                                        //SendFileToClient(clientSocket, requestedFile);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for executing file " + filePathRequest + " has accepted. Time: " + DateTime.Now);
                                        client.sck.Send(Encoding.Default.GetBytes("SERVER: File is executing..."));

                                        
                                    }
                                    else
                                    {
                                        byte[] denyMessage = Encoding.Default.GetBytes("SERVER WARNING: You do not have permission to execute this file.");
                                        client.sck.Send(denyMessage);
                                        listBox1.Items.Add("Request from client: " + client.clientName + " for executing file " + filePathRequest + " has refused. Time: " + DateTime.Now);
                                    }
                                }
                            }

                            break;
                        }
                    }
                });
            }
            catch (StackOverflowException soe)
            {

            }

        }


        //metoda per dergimin e files nga serveri te klienti
        private void SendFileToClient(Socket clientSocket, string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    clientSocket.Send(Encoding.Default.GetBytes("ERROR: File not found"));
                    return;
                }

                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Send the length of the file name
                byte[] fileNameBytes = Encoding.Default.GetBytes(Path.GetFileName(filePath));
                clientSocket.Send(BitConverter.GetBytes(fileNameBytes.Length)); // File name length
                clientSocket.Send(fileNameBytes); // File name




                // Send the file size
                long fileSize = fileBytes.Length;
                clientSocket.Send(BitConverter.GetBytes(fileSize)); // File size

                // Send the file data in chunks
                int chunkSize = 8192; // 8 KB per chunk
                int bytesSent = 0;
                while (bytesSent < fileSize)
                {
                    int bytesToSend = (int)Math.Min(chunkSize, fileSize - bytesSent);
                    clientSocket.Send(fileBytes, bytesSent, bytesToSend, SocketFlags.None);
                    bytesSent += bytesToSend;
                }


            }
            catch (Exception ex)
            {
                clientSocket.Send(Encoding.Default.GetBytes("ERROR: Failed to send file"));
            }
        }


        //Metoda per shkrim ne file nga klienti
        public void writeToFile(string filePath, string textToWrite)
        {
            string fullFilePath = @txtSharedFolderPath.Text; // Specify the full file path
            fullFilePath += filePath;
            try
            {
                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(fullFilePath, append: false)) // Set 'append' to 'false' to overwrite the file
                {
                    writer.WriteLine(textToWrite);
                }
                MessageBox.Show("File has written successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while writing in file !");
            }
        }




        //Metoda per ekzekutim te skriptave
        string ExecuteScript(string scriptPath)
        {
            string scriptType = scriptPath.Substring(scriptPath.Length - 2);
            string fullScriptPath = @txtSharedFolderPath.Text; // Specify the full file path
            fullScriptPath += scriptPath;

            try
            {
                // Check if the script or executable exists
                if (File.Exists(fullScriptPath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    string commandArguments = "";

                    // Based on the script type, set up the appropriate process
                    switch (scriptType.ToLower())
                    {
                        case "pp":
                            // For C++, assume that scriptPath is the path to the executable (.exe or .out)
                            startInfo.FileName = fullScriptPath; // Just execute the C++ program directly
                            break;

                        case "js":
                            // For JavaScript, assume that Node.js is installed, and scriptPath is the path to the .js file
                            startInfo.FileName = "node";
                            commandArguments = fullScriptPath; // Run the script with Node.js
                            break;

                        case "ss":
                            // For Java, assume that the scriptPath is the compiled .class or .jar file
                            if (fullScriptPath.EndsWith(".jar"))
                            {
                                // If it's a JAR file, use the `-jar` option
                                startInfo.FileName = "java";
                                commandArguments = $"-jar {fullScriptPath}";
                            }
                            else if (fullScriptPath.EndsWith(".class"))
                            {
                                // For Java, assume that the scriptPath is the compiled .class or .jar file
                                startInfo.FileName = "java";
                                string className = Path.GetFileNameWithoutExtension(fullScriptPath); // Class name is the file name without extension

                                // If it's a .class file, set classpath appropriately
                                string directory = Path.GetDirectoryName(fullScriptPath);
                                commandArguments = $"-cp \"{directory}\" {className}";
                            }
                            break;

                        case "py":
                            // For Python, assume scriptPath is the path to the Python script
                            startInfo.FileName = "python"; // Make sure Python is in the system's PATH
                            commandArguments = fullScriptPath; // Run the script with Python
                            break;

                        default:
                            // If we don't recognize the script type, return an error message
                            return $"Unknown script type: {scriptType}";
                    }



                    startInfo.Arguments = commandArguments;
                    startInfo.CreateNoWindow = true;  // Do not create a new window for the process
                    startInfo.UseShellExecute = false;  // Do not use the system shell for execution
                    startInfo.RedirectStandardOutput = true;  // Capture the output of the script
                    startInfo.RedirectStandardError = true;  // Capture any error output

                    Process process = new Process();
                    process.StartInfo = startInfo;

                    // Start the process (this will execute the script)
                    process.Start();

                    // Capture and return the output of the script
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // If there's no output, return the error message instead
                    if (string.IsNullOrEmpty(output))
                    {
                        output = error;
                    }


                    process.WaitForExit(); // Wait for the process to finish

                    return output;

                }
                else
                {
                    return "Script file not found.";
                    listBox1.Items.Add("Script file not found");
                    MessageBox.Show("Scrit nuk u gjet");
                }
            }
            catch (Exception ex)
            {
                return $"Error executing script: {ex.Message}";
                MessageBox.Show("Erorrr nuk u gjet");
            }
        }




        // butoni per te filluar degjimin
        private void button1_Click(object sender, EventArgs e)
        {
            string portText = txtPort.Text;
            int.TryParse(portText, out int port);
            listener = new Listener(port);

            //listener.setPort(port);
            listener.SocketAccepted += new Listener.SocketAcceptedHandler(listener_SocketAccepted);
            Load += new EventHandler(Main_Load);
            listener.Start();

            btnListen.BackColor = Color.LightBlue;
            sharedFolderPath = txtSharedFolderPath.Text.ToString();
        }



        //Metod per menaxhimin e permissions
        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    string sharedFolderPath = txtSharedFolderPath.Text;
                    for (int i = 0; i < listClients.Items.Count; i++)
                    {
                        Client client = listClients.Items[i].Tag as Client;

                        if (client.ID == txtClient.Text)
                        {
                            if (checkRead.Checked && !checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Read);
                            }
                            else if (!checkRead.Checked && checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Write);
                            }
                            else if (!checkRead.Checked && !checkWrite.Checked && checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.Execute);
                            }
                            else if (checkRead.Checked && checkWrite.Checked && !checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.ReadWrite);
                            }
                            else if (checkRead.Checked && checkWrite.Checked && checkExecute.Checked)
                            {
                                client.Permissions.SetPermissions(sharedFolderPath, FilePermissions.All);
                            }

                            break;
                        }
                    }
                });
            }
            catch (StackOverflowException soe)
            {

            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {

        }

        //////////////////////////////////////////
        private void button2_Click(object sender, EventArgs e)
        {
            string sharedFolderPath = txtSharedFolderPath.Text;
            string files = "~";
            try
            {
                // Get all file paths in the directory
                string[] filePaths = Directory.GetFiles(sharedFolderPath);

                // Loop through each file and print the file name
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath); // Extract just the file name
                    files += fileName + "~";

                }
                txtFiles.Text = files;
                listBox1.Items.Add(files);




                byte[] fileNameBytes = Encoding.Default.GetBytes(files);
                for (int i = 0; i < listClients.Items.Count; i++)
                {
                    Client client = listClients.Items[i].Tag as Client;

                    if (client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Read)
                        || client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Write)
                        || client.Permissions.HasPermission(sharedFolderPath, FilePermissions.Execute)
                        || client.Permissions.HasPermission(sharedFolderPath, FilePermissions.ReadWrite)
                        || client.Permissions.HasPermission(sharedFolderPath, FilePermissions.All))
                    {
                        client.sck.Send(fileNameBytes);
                    }


                }

            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Error while sending file names !");
            }
        }
    }
}
