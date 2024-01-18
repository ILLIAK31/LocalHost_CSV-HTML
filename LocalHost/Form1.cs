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
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace LocalHost
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread listenerThread;
        private bool isServerRunning = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void button_port_Click(object sender, EventArgs e)
        {
            if (!isServerRunning)
            {
                int port = int.Parse(port2.Text);
                string directoryPath = path.Text;

                // Convert CSV files to HTML files
                ConvertCsvFilesToHtml(directoryPath);

                // Start the server
                listenerThread = new Thread(() => StartServer(port, directoryPath));
                listenerThread.Start();

                isServerRunning = true;
                button_port.Enabled = false;
                button_stop.Enabled = true;
            }
        }
        private void ConvertCsvFilesToHtml(string directoryPath)
        {
            try
            {
                int fileNumber = 1;

                foreach (string csvFilePath in Directory.EnumerateFiles(directoryPath, "*.csv"))
                {
                    // Read CSV content and convert it to HTML
                    string csvContent = File.ReadAllText(csvFilePath);
                    string htmlContent = ConvertCsvToHtml(csvContent);

                    // Save the HTML content to a corresponding HTML file
                    string htmlFileName = $"file{fileNumber}.html";
                    string htmlFilePath = Path.Combine(directoryPath, htmlFileName);
                    File.WriteAllText(htmlFilePath, htmlContent);

                    fileNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting CSV to HTML: {ex.Message}");
            }
        }

        private string ConvertCsvToHtml(string csvContent)
        {
            int i = 0;
            string tag1 = "<th>", tag2 = "</th>", Text2 = "\n</table>\n</body>\n</html>", Text1 = "<!DOCTYPE html>\n<html>\n<head>\n<style>\ntable\n{\nfont-family: arial, sans-serif;\nborder-collapse: collapse;\nwidth: 100%;\n}\ntd, th\n{\nborder: 1px solid #dddddd;\ntext-align: left;\npadding: 8px;\n}\ntr:nth-child(even)\n{\nbackground-color: #c9c9c9;\n}\n</style>\n</head>\n<body>\n<table>";

            foreach (var line1 in csvContent.Split('\n'))
            {
                Text1 += "\n<tr>\n";
                var csv2 = line1.Split(';');
                foreach (var line2 in csv2)
                {
                    Text1 += (tag1 + line2 + tag2);
                }
                Text1 += "\n</tr>";
                if (i == 0)
                {
                    tag1 = "<td>";
                    tag2 = "</td>";
                }
                ++i;
            }

            return Text1 + Text2;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if (isServerRunning)
            {
                tcpListener.Stop();
                listenerThread.Join();

                isServerRunning = false;
                button_port.Enabled = true;
                button_stop.Enabled = false;
            }
        }
        private void StartServer(int port, string directoryPath)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();

                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client, directoryPath));
                    clientThread.Start();
                }
            }
            catch (ThreadAbortException)
            {
                // Thread was aborted, likely due to stopping the server
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void HandleClient(TcpClient tcpClient, string directoryPath)
        {
            try
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                using (StreamReader reader = new StreamReader(networkStream))
                using (StreamWriter writer = new StreamWriter(networkStream))
                {
                    string request = reader.ReadLine();
                    string[] requestParts = request.Split(' ');

                    if (requestParts.Length >= 2 && requestParts[0] == "GET")
                    {
                        string filePath = GetFilePath(directoryPath, requestParts[1]);
                        string response = GenerateResponse(filePath);
                        writer.Write(response);
                    }
                    else
                    {
                        writer.Write("HTTP/1.1 400 Bad Request\r\n\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
                SendErrorResponse(tcpClient.GetStream(), 500);
            }
            finally
            {
                tcpClient.Close();
            }
        }

        private void SendErrorResponse(Stream stream, int statusCode)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write($"HTTP/1.1 {statusCode} Internal Server Error\r\n\r\n");
            writer.Flush();
        }


        private string GetFilePath(string directoryPath, string requestedPath)
        {
            if (string.IsNullOrEmpty(requestedPath) || requestedPath == "/")
            {
                // Serve default index.html
                return Path.Combine(directoryPath, "index.html");
            }

            string fileName = requestedPath.TrimStart('/');
            string filePath = Path.Combine(directoryPath, fileName);

            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                // If the requested file doesn't exist, serve the default 404.html
                return Path.Combine(directoryPath, "404.html");
            }
        }


        private string GenerateResponse(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);

                    return $"HTTP/1.1 200 OK\r\n" +
                           $"Content-Length: {fileContent.Length}\r\n" +
                           $"Content-Type: text/html; charset=utf-8\r\n\r\n" +
                           $"{fileContent}";
                }
                else
                {
                    return "HTTP/1.1 404 Not Found\r\n\r\n";
                }
            }
            catch
            {
                return "HTTP/1.1 500 Internal Server Error\r\n\r\n";
            }
        }


        private void button_folder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"C:\";

            // Show the folder browser dialog
            DialogResult result = folderBrowserDialog1.ShowDialog();

            // Check if the user clicked OK
            if (result == DialogResult.OK)
            {
                // Get the selected folder path
                string selectedPath = folderBrowserDialog1.SelectedPath;

                // Do something with the selected folder path
                path.Text = selectedPath;
            }
        }
    }
}
