using System;
using System.Net.Sockets;
using System.Threading;
using Server;
using ServerData;

namespace Handler {
    class ClientData {
        public Socket ClientSocket;
        public Thread ClientThread;
        public string ID;

        public ClientData() 
        {
            ID = Guid.NewGuid().ToString();
            ClientThread = new Thread (RequestServer.Data_IN);
            ClientThread.Start(ClientSocket);
            sendRegistrationPacket();
        }

        public ClientData(Socket ClientSocket) 
        {
            this.ClientSocket = ClientSocket;
            ID = Guid.NewGuid().ToString();
            ClientThread = new Thread (RequestServer.Data_IN);
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