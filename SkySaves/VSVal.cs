using System;

namespace SkySaves
{
    public class VSVal
    {
        public enum ValType : byte
        {
            UInt8,
            UInt16,
            UInt32
        }

        private byte uint8Value;
        private ushort uint16Value;
        private uint uint32Value;

        public ValType Type { get; private set; }

        public object ValueByObject
        {
            get
            {
                switch (Type)
                {
                    case ValType.UInt8:
                        return uint8Value;

                    case ValType.UInt16:
                        return uint16Value;

                    case ValType.UInt32:
                        return uint32Value;
                        
                    default:
                        throw new Exception("Invalid VSVal type");
                }
            }
        }

        public int ValueByInt
        {
            get
            {
                switch (Type)
                {
                    case ValType.UInt8:
                        return uint8Value;

                    case ValType.UInt16:
                        return uint16Value;

                    case ValType.UInt32:
                        return (int)uint32Value;

                    default:
                        throw new Exception("Invalid VSVal type");
                }
            }
        }

        public VSVal(byte value)
        {
            Type = ValType.UInt8;
            uint8Value = value;
        }

        public VSVal(ushort value)
        {
            Type = ValType.UInt16;
            uint16Value = value;
        }

        public VSVal(uint value)
        {
            Type = ValType.UInt32;
            uint32Value = value;
        }
    }
}
