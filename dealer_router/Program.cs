using dealer_router;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Text;
using ZeroMQ;

namespace ZMQGuide
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var server = new ResponseSocket("@tcp://localhost:5556")) 
            using (var client = new RequestSocket(">tcp://localhost:5556"))  
            {
                Console.WriteLine("Unesite nick");

                var nick = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Unesite poruku");
                    var messageContent = Console.ReadLine();
                    Message message = new Message(nick, messageContent);
                    client.SendFrame(message.ToString());

                    string m1 = server.ReceiveFrameString();
                    string[] nickname = m1.Split(',');
                    Console.WriteLine("From Client: {0},message: {1}", nickname[0],nickname[1]);

                    server.SendFrame("Hi " + nickname[0]);

                    string m2 = client.ReceiveFrameString();
                    Console.WriteLine("From Server: {0}", m2);

                }

            }
        }
    }
}