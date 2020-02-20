using System;

namespace SkySaves
{
    public class RefID
    {
        public readonly byte Byte0;
        public readonly byte Byte1;
        public readonly byte Byte2;

        public RefID(byte byte0, byte byte1, byte byte2)
        {
            Byte0 = byte0;
            Byte1 = byte1;
            Byte2 = byte2;
        }
    }
}
