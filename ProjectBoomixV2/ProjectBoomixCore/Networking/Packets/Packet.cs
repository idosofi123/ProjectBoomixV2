using System;
using System.IO;
using ProtoBuf;

namespace ProjectBoomixCore.Networking.Packets {

    [ProtoContract]
    [ProtoInclude(1, typeof(ClientPacket))]
    [ProtoInclude(2, typeof(ServerPacket))]
    public abstract class Packet {
        
        public byte[] Serialize() {
            byte[] data;

            using (MemoryStream stream = new MemoryStream()) {
                Serializer.Serialize(stream, this);
                return stream.ToArray();
            }
        }

        public static Packet Deserialize(byte[] packetBytes) {
            return Serializer.Deserialize<Packet>(new MemoryStream(packetBytes));
        }
    }
}
