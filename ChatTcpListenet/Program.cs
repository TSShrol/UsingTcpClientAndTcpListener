using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatTcpListener
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any,8008);
            NetworkStream stream = null;
            try
            {
                tcpListener.Start();
                byte[] data = new byte[1024];
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Console.WriteLine($"Client connected... {tcpClient.Client.RemoteEndPoint}");
                    stream = tcpClient.GetStream();
                    byte[] myReadBuffer = new byte[1024];
                    StringBuilder builder = new StringBuilder();
                    int bytes= 0;
                    while (true) {
                        do
                        {
                            bytes = stream.Read(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        } while (stream.DataAvailable);

                        Console.WriteLine("You received the following message : " + builder);

                        string message = Encoding.Unicode.GetString(data);
                        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} :: {message} ");
                        //stream.Flush();

                        data = Encoding.Unicode.GetBytes("Hello, client");
                        stream.Write(data);
                        //stream.Flush();
                        builder.Clear();
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                tcpListener.Stop();
            }
        }
    }
}
