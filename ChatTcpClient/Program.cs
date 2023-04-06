using System;
using System.Net.Sockets;
using System.Text;

namespace ChatTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();


            byte[] data = new byte[1024];
            NetworkStream stream = null;
            try
            {
                tcpClient.Connect("127.0.0.1",8008);
                while (true)
                {
                    Console.Write("Input message>> ");
                    var message = Console.ReadLine();
                    //var message = "Hello, server";
                    data = Encoding.Unicode.GetBytes(message);
                    stream = tcpClient.GetStream();
                    stream.Write(data);

                    byte[] myReadBuffer = new byte[1024];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (stream.DataAvailable);

                    Console.WriteLine("You received the following message : " + builder);

                }
            }
            catch (SocketException ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null) stream.Close();
                tcpClient.Close();
            }
            Console.ReadKey();
        }
    }
}
