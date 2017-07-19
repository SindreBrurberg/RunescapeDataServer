using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using Handler;
using Collector;
using Sql;
using ServerData;

namespace Server
{
    class Program
    {
        private static Socket ListenerSocket;
        private static List<ClientData> _Clients = new List<ClientData>();

        private static List<string> clans;
        static void Main(string[] args)
        {
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Packet.IPV4Address()), 4242);
            ListenerSocket.Bind(ep);
            Thread Listentrd = new Thread(ListenThread);
            Listentrd.Start();

            Console.ReadLine();
            config();
            uppdateLoop(null);
            Console.ReadLine();
            var testTimer = new Timer(uppdateLoop, null, MillisecondsToNextHalfHouer(), 30*60*1000);
            Console.ReadLine();
        }

        public static void Data_IN(object Socket) {
            Socket ClientSocket = (Socket)Socket;
            byte[] buffer;
            int readBytes;
            try {
                for (;;) 
                {
                    buffer = new byte[ClientSocket.SendBufferSize];
                    readBytes = ClientSocket.Receive(buffer);
                    if (readBytes > 0) 
                    {
                        byte[] readBuffer = new byte[readBytes]; 
                        Array.Copy(buffer, readBuffer, readBytes);
                        dataManager(new Packet(readBuffer));
                    }
                }
            } catch (SocketException e) {
                Console.WriteLine(e);
            }
        }

        public static void dataManager(Packet p)
        {
            switch (p.PacketType)
            {
                case PacketType.chat:
                    foreach (ClientData c in _Clients)
                    {
                        c.ClientSocket.Send(p.toBytes());
                    }
                    break;
            }
        }

        private static void ListenThread() {
            for(;;) 
            {
                ListenerSocket.Listen(0);
                _Clients.Add(new ClientData(ListenerSocket.Accept()));
            }
        }
        private static int MillisecondsToNextHalfHouer() {
            DateTime now = DateTime.Now;
            return ((60 - now.Minute) % 30 * 60 - now.Second) * 1000 - now.Millisecond;
        }
        private static void config() {
            ConfigFile.init();
            Console.WriteLine("Config File Loaded");
            clans = Sql.Object.clans();
        }
        private static void uppdateLoop(object stateInfo) {
            foreach (string clan in clans) {
                Clan consentus = new Clan(clan);
            }
        }
    }
    class ClientData {
        public Socket ClientSocket;
        public Thread ClientThread;
        public string ID;

        public ClientData() 
        {
            ID = Guid.NewGuid().ToString();
            ClientThread = new Thread (Program.Data_IN);
            ClientThread.Start(ClientSocket);
            sendRegistrationPacket();
        }

        public ClientData(Socket ClientSocket) 
        {
            this.ClientSocket = ClientSocket;
            ID = Guid.NewGuid().ToString();
            ClientThread = new Thread (Program.Data_IN);
            ClientThread.Start(ClientSocket);
            sendRegistrationPacket();
        }
        public void sendRegistrationPacket()
        {
            Packet p = new Packet(PacketType.Registration, "server");
            p.Gdata.Add(ID);
            ClientSocket.Send(p.toBytes());
        }
    }
}

