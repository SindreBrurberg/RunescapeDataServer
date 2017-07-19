using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ServerData;

namespace Client
{
    class Client
    {
        public static Socket master;
        public static string name;
        public static string id;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Your Name: ");
            name = Console.ReadLine();

            Console.WriteLine("Enter server IP:");
            string IP = Console.ReadLine();
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), 4242);
            master.Connect(ep);
            Thread t = new Thread(data_IN);
            t.Start();

            for(;;)
            {
                Console.Write("::>");
                string input = Console.ReadLine();

                Packet p = new Packet(PacketType.chat, id);
                p.Gdata.Add(name);
                p.Gdata.Add(input);
                master.Send(p.toBytes());
            }

        }
        static void data_IN()
        {
            byte[] buffer;
            int readBytes;

            for(;;)
            {
                try
                {
                    buffer = new byte[master.SendBufferSize];
                    readBytes = master.Receive(buffer);

                    if (readBytes > 0)
                    {
                        byte[] readBuffer = new byte[readBytes]; 
                        Array.Copy(buffer, readBuffer, readBytes);
                        DataManager(new Packet(readBuffer));
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Server Lost!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

        }
        static void DataManager(Packet p)
        {
            switch (p.PacketType)
            {
                case PacketType.Registration:
                    Console.WriteLine("Connected to Server!");
                    id = p.Gdata[0];
                    break;
                case PacketType.chat:
                    try {
                        Console.WriteLine(p.SenderID);
                        Console.WriteLine(p.Gdata[0] + ": " + p.Gdata[1]);
                    } catch (NullReferenceException e) {
                        Console.WriteLine(e);
                    }
                    break;
            }
        }
    }
}
