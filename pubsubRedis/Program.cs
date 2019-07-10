using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pubsubRedis
{
    class Program
    {
        private static ConnectionMultiplexer connection =ConnectionMultiplexer.Connect("localhost");
        private static  string ChatChannel =string.Empty;
        private static string userName = string.Empty;
        readonly static Random rnd = new Random();

        static void Main(string[] args)
        {
            List<string> genres = new List<string>();
            genres.Add("games");
            genres.Add("music");
            for (int i = 0; i < genres.Count; i++)
            {
                Console.WriteLine($"{i} {genres[i]}");
            }
            Console.WriteLine("izaberite temu");
            int index = Convert.ToInt32(Console.ReadLine());


            ChatChannel = genres[index];
            Console.Write("Unesite ime: ");
            userName = Console.ReadLine();
            var pubsub = connection.GetSubscriber();
            pubsub.Subscribe(ChatChannel, (channel, message) => MessageAction(message));

            pubsub.Publish(ChatChannel, $"'{userName}' se pridruzio");
            while (true)
            {
                pubsub.Publish(ChatChannel, $"{userName}: {Console.ReadLine()}  " +
                  $"({DateTime.Now.Hour}:{DateTime.Now.Minute})");
            }
        }

        static void MessageAction(string message)
        {
            int initialCursorTop = Console.CursorTop;
            int initialCursorLeft = Console.CursorLeft;

            Console.MoveBufferArea(0, initialCursorTop, Console.WindowWidth,
                1, 0, initialCursorTop + 1);
            Console.CursorTop = initialCursorTop;
            Console.CursorLeft = 0;

            // Print the message here
            Console.WriteLine(message);

            Console.CursorTop = initialCursorTop + 1;
            Console.CursorLeft = initialCursorLeft;
        }
    }
}
