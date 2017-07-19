using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ServerData
{
    public class Packet
    {
        public List<string> Gdata;
        public string SenderID;
        public PacketType PacketType;

        public Packet(PacketType PacketType, string SenderID) 
        {
            Gdata = new List<string>();
            this.SenderID = SenderID;
            this.PacketType = PacketType;
        }
        public Packet (byte[] bytes) {
            Gdata = new List<string>();
            string data = Encoding.UTF8.GetString(bytes);
            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string obj = "";
            foreach (var line in lines) {
                try {
                    if (line.EndsWith("{")) {
                        obj = line.Remove(line.IndexOf(":"));
                    } else if (line.StartsWith("}")) {
                        obj = "";
                    } else {
                        switch (obj) {
                            case "Gdata":
                                Gdata.Add(line.Trim());
                                break;
                            default:
                                if (line.Contains("SenderID")) {
                                    this.SenderID = line.Substring(line.IndexOf(":") + 1).Replace(" ", "");
                                } else if (line.Contains("PacketType")) {
                                    this.PacketType = (PacketType)Enum.Parse(typeof(PacketType), line.Substring(line.IndexOf(":") + 1).Replace(" ", ""));
                                }
                                break;
                        }
                        
                    }
                } catch (NullReferenceException e) {
                    Console.WriteLine(e);
                }
            }
        }
        public byte[] toBytes() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SenderID: " + SenderID);
            sb.AppendLine("PacketType: " + PacketType.ToString());
            sb.AppendLine("Gdata: {");
            try {
                foreach (string s in Gdata) {
                    sb.AppendLine("\t" + s);
                }
            } catch (NullReferenceException e) {
                    Console.WriteLine(e);
                }
            sb.AppendLine("}");
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
        public static string IPV4Address() {
                return "127.0.0.1";
        }
    }

    public enum PacketType 
    {
        Registration,
        chat
    }
}
