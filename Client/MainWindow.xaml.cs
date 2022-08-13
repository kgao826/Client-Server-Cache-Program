using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string clientFolderName = "\\Client_Files";
        const int CachePortNo = 8001;
        const string CacheIP = "127.0.0.2";
        public MainWindow()
        {
            InitializeComponent();
            ListBoxFiles();
        }
        public static string getParentDirectory()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            return projectDirectory;
        }

        public static void WriteToFile(string filenamePath, string fileContents)
        {
            File.WriteAllText(filenamePath, fileContents);
        }

        public static string GetCacheData(string content)
        {
            TcpClient client = new(CacheIP, CachePortNo);
            int clientBufferSize = client.ReceiveBufferSize;
            byte[] message = Encoding.UTF8.GetBytes(content);
            client.GetStream().Write(message, 0, message.Length);
            byte[] buffer = new byte[clientBufferSize];
            int bytesAmt = client.GetStream().Read(buffer, 0, clientBufferSize);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesAmt);
            client.Close();
            return data;
        }

        private void ListBoxFiles()
        {
            string response = GetCacheData("GetFileList");
            string[] filenames = response.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string file in filenames)
            {
                filenamesListBox.Items.Add(file);
            }

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (filenamesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item first!");
            }
            else
            {
                string currentSelectedFile = filenamesListBox.SelectedItem.ToString();
                string directory = getParentDirectory();

                string fileContents = GetCacheData("Download: " + currentSelectedFile);
                WriteToFile(directory + clientFolderName + "\\" + currentSelectedFile, fileContents);
                MessageBox.Show("File Downloaded!");
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            string directory = getParentDirectory();
            if (filenamesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item first!");
            }
            else
            {
                string currentSelectedFile = filenamesListBox.SelectedItem.ToString();
                string file = directory + clientFolderName + "\\" + currentSelectedFile;
                if (File.Exists(file))
                {
                    string fileContents = File.ReadAllText(file);
                    ViewBox.Text = fileContents;
                    CurrentFileName.Text = currentSelectedFile;
                }
                else
                {
                    ViewBox.Text = "File not downloaded yet, please download file first before viewing!";
                }
            }

        }
    }
}
