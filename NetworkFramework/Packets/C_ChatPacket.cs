﻿using H00N.Network;
using System;

namespace Packets
{
    public class C_ChatPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_ChatPacket; // 패킷 아이디

        public string message; // 보낸 메세지

        public override void Deserialize(ArraySegment<byte> buffer) // 역직렬화
        {
            ushort process = 0; // 처리한 길이

            process += sizeof(ushort); // 패킷 사이즈
            process += sizeof(ushort); // 패킷 아이디

            process += PacketUtility.ReadStringData(buffer, process, out this.message); // message 삽입
        }

        public override ArraySegment<byte> Serialize() // 직렬화
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024); // 1024만큼 버퍼 발급

            ushort process = 0; // 처리한 길이
            process += sizeof(ushort); // 패킷 사이즈 넣을 공간 확보

            process += PacketUtility.AppendUShortData(this.ID, buffer, process); // ID 삽입
            process += PacketUtility.AppendStringData(this.message, buffer, process); // message 삽입
            PacketUtility.AppendUShortData(process, buffer, 0); // 아까 확보해둔 공간에 전체 패킷의 길이 할당

            return UniqueBuffer.Close(process); // 버퍼 반환 (사용한 만큼만 재발급)
        }
    }
}
