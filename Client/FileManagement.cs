using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Connections_Server
{
    internal class FileManagement
    {

        //metoda per pranimin (download) te files
        public void ReceiveFileFromServer(Socket clientSocket, string savePath)
        {
        try 
        {
                // Receive the file name length (to know how much to read)
                byte[] fileNameLengthBytes = new byte[4];
                clientSocket.Receive(fileNameLengthBytes);
                int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);

                // Receive the file name
                byte[] fileNameBytes = new byte[fileNameLength];
                clientSocket.Receive(fileNameBytes);
                string fileName = Encoding.Default.GetString(fileNameBytes);

                // Receive the file size
                byte[] fileSizeBytes = new byte[8];
                clientSocket.Receive(fileSizeBytes);
                long fileSize = BitConverter.ToInt64(fileSizeBytes, 0);

                // Combine save path and filename to form the full file path
                string fullFilePath = Path.Combine(savePath, fileName);

                // Create a file stream to write the file data to the specified path
                using (FileStream fs = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024]; // 1 KB buffer
                    long totalBytesReceived = 0;

                    // Receive the file in chunks
                    while (totalBytesReceived < fileSize)
                    {
                        int bytesReceived = clientSocket.Receive(buffer);
                        fs.Write(buffer, 0, bytesReceived);
                        totalBytesReceived += bytesReceived;
                    }

                    Console.WriteLine($"File received and saved to: {fullFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving file: {ex.Message}");
            }
        }
    }
}
