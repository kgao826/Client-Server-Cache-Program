using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Cache
{
    public partial class MainWindow : Window
    {
        public const string cacheFolder = "\\Cache_Files";
        public const string cacheLogFileName = "CacheLog.txt";
        const int ServerPortNo = 8000;
        const string ServerIP = "127.0.0.1";
        const int CachePortNo = 8001;
        const string CacheIP = "127.0.0.2";
        public MainWindow()
        {
            InitializeComponent();
            ListCacheFiles();
            string logContents = ReadLog();
            CacheLogBox.Text = logContents;
            RunCache();
        }
        public static string GetServerData(string content)
        {
            TcpClient server = new(ServerIP, ServerPortNo);
            int serverBufferSize = server.ReceiveBufferSize;
            byte[] message = Encoding.UTF8.GetBytes(content);
            server.GetStream().Write(message, 0, message.Length);
            byte[] buffer = new byte[serverBufferSize];
            int bytesAmt = server.GetStream().Read(buffer, 0, serverBufferSize);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesAmt);
            server.Close();
            return data;
        }

        public static async Task RunCache()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Cache is running");
                IPAddress cacheAddress = IPAddress.Parse(CacheIP);
                TcpListener listener = new TcpListener(cacheAddress, CachePortNo);
                listener.Start();
                while (true)
                {
                    string fileContents = "";
                    TcpClient client = listener.AcceptTcpClient();
                    int clientBufferSize = client.ReceiveBufferSize;
                    byte[] buffer = new byte[clientBufferSize];
                    int bytesAmt = client.GetStream().Read(buffer, 0, clientBufferSize);
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesAmt);
                    if (data == "GetFileList")
                    {
                        fileContents = GetServerData("GetFileList");
                        byte[] response = System.Text.Encoding.UTF8.GetBytes(fileContents);
                        client.GetStream().Write(response, 0, response.Length);
                    }
                    else if (data.Contains("Download: ")) //format of downlaod = Download: Filename
                    {
                        string filename = data.Substring(10);
                        bool inCache = false;
                        inCache = IsInCache(filename);
                        string log = Log(filename);
                        if (inCache)
                        {
                            fileContents = RetrieveFromCache(filename);
                            WriteToLog(log);
                            WriteToLog("response: cached file " + filename);
                        }
                        else
                        {
                            fileContents = GetServerData("Download: " + filename);
                            PutInCache(filename, fileContents);
                            WriteToLog(log);
                            WriteToLog("response: file " + filename + " downloaded from the server");
                        }
                        byte[] response = System.Text.Encoding.UTF8.GetBytes(fileContents);
                        client.GetStream().Write(response, 0, response.Length);
                    }
                    client.Close();
                }
            });
        }

        public static void WriteToLog(string content)
        {
            string directory = getParentDirectory();
            using StreamWriter file = new(directory + "\\" + cacheLogFileName, append: true);
            file.WriteLineAsync(content);
        }

        public static string getParentDirectory()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }

        public static bool IsInCache(string filename)
        {
            bool isInCache = false;
            string directory = getParentDirectory();
            if (File.Exists(directory + cacheFolder + "\\" + filename))
            {
                isInCache = true;
            }
            return isInCache;
        }

        public static string RetrieveFromCache(string filename)
        {
            string directory = getParentDirectory();
            string fileContents;
            fileContents = File.ReadAllText(directory + cacheFolder + "\\" + filename);
            return fileContents;
        }

        public static void PutInCache(string filename, string fileContents)
        {
            string directory = getParentDirectory();
            File.WriteAllText(directory + cacheFolder + "\\" + filename, fileContents);
        }

        public static string Log(string filename)
        {
            DateTime time = DateTime.Now;
            string log = "user request: file " + filename + " at " + time.ToString();
            return log;
        }

        public static string ReadLog()
        {
            string directory = getParentDirectory();
            string fileContents = "No logs yet!";
            string file = directory + "\\" + cacheLogFileName;
            if (File.Exists(file))
            {
                fileContents = File.ReadAllText(file);
            }
            return fileContents;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            string dir = getParentDirectory();
            DirectoryInfo directory = new DirectoryInfo(dir + "\\Cache_Files");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            File.Delete(dir + "\\" + cacheLogFileName);
            MessageBox.Show("Cache items deleted");
            CachedFileListBox.Items.Clear();
            string logContents = ReadLog();
            CacheLogBox.Text = logContents;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string logContents = ReadLog();
            CacheLogBox.Text = logContents;
            UpdateListCacheFiles();
        }

        private void ListCacheFiles()
        {
            List<string> filenames = GetCacheFiles();
            foreach (string filename in filenames)
            {
                CachedFileListBox.Items.Add(filename);
            }
        }

        private void UpdateListCacheFiles()
        {
            CachedFileListBox.Items.Clear();
            List<string> filenames = GetCacheFiles();
            foreach (string filename in filenames)
            {
                CachedFileListBox.Items.Add(filename);
            }
        }

        public static List<string> GetCacheFiles()
        {
            string directory = getParentDirectory();
            string[] rawfilenames = Directory.GetFiles(directory + cacheFolder);
            List<string> filenames = new();
            foreach (string filename in rawfilenames)
            {
                filenames.Add(System.IO.Path.GetFileName(filename));
            }
            return filenames;
        }
    }
}
