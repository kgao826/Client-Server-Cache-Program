using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        const int PortNo = 8000;
        const string ServerIP = "127.0.0.1";
        public const string serverFolderName = "\\Server_Files";

        static void Main()
        {
            Console.WriteLine("Server is running");
            IPAddress serverAddress = IPAddress.Parse(ServerIP);
            TcpListener listener = new TcpListener(serverAddress, PortNo);
            listener.Start();
            while (true)
            {
                TcpClient server = listener.AcceptTcpClient();
                int clientBufferSize = server.ReceiveBufferSize;
                byte[] buffer = new byte[clientBufferSize];
                int bytesAmt = server.GetStream().Read(buffer, 0, clientBufferSize);
                string data = Encoding.ASCII.GetString(buffer, 0, bytesAmt);
                if (data.Contains("GetFileList"))
                {
                    List<string> fileList = GetFileName();
                    string convertList = "";
                    foreach (string file in fileList)
                    {
                        convertList += file + "\n";
                    }
                    convertList = convertList.Substring(0,convertList.Length - 1);
                    byte[] response = System.Text.Encoding.UTF8.GetBytes(convertList);
                    server.GetStream().Write(response, 0, response.Length);
                }
                else if (data.Contains("Download: ")) //format of downlaod = Download: Filename
                {
                    string filename = data.Substring(10);
                    string fileContents = DownloadFile(filename);
                    byte[] response = System.Text.Encoding.UTF8.GetBytes(fileContents);
                    server.GetStream().Write(response, 0, response.Length);
                }
                server.Close();
            }
        }
        
        public static string getParentDirectory()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }
        public static List<string> GetFileName()
        {
            string directory = getParentDirectory();
            string[] rawfilenames = Directory.GetFiles(directory + serverFolderName);
            List<string> filenames = new();
            foreach (string filename in rawfilenames)
            {
                filenames.Add(Path.GetFileName(filename));
            }
            Console.WriteLine("Requested file names");
            return filenames;
        }

        public static string DownloadFile(string filename)
        {
            string directory = getParentDirectory();
            string fileContents;
            fileContents = File.ReadAllText(directory + serverFolderName + "\\" + filename);
            Console.WriteLine("Downloaded file: " + filename);
            return fileContents;
        }
    }
}
