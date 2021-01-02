using System;
using System.IO;
using System.Text;

namespace SkySaves
{
    public static class BinHelp
    {
        public static byte ReadByte(ref FileStream fileStream)
        {
            return (byte)fileStream.ReadByte();
        }

        public static ushort ReadUInt16(ref FileStream fileStream)
        {
            byte[] bytes =
            {
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte()
            };

            return BitConverter.ToUInt16(bytes, 0);
        }

        public static uint ReadUInt32(ref FileStream fileStream)
        {
            byte[] bytes =
            {
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte()
            };

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static float ReadFloat(ref FileStream fileStream)
        {
            byte[] bytes =
            {
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte(),
                (byte)fileStream.ReadByte()
            };

            return BitConverter.ToSingle(bytes, 0);
        }

        public static ushort GetUInt16(byte[] data, ref int index)
        {
            return BitConverter.ToUInt16(new byte[] { data[index++],
                data[index++] }, 0);
        }

        public static uint GetUInt32(byte[] data, ref int index)
        {
            return BitConverter.ToUInt32(new byte[] { data[index++],
                data[index++], data[index++], data[index++] }, 0);
        }

        public static int GetInt32(byte[] data, ref int index)
        {
            return BitConverter.ToInt32(new byte[] { data[index++],
                data[index++], data[index++], data[index++] }, 0);
        }

        public static float GetFloat(byte[] data, ref int index)
        {
            return BitConverter.ToSingle(new byte[] { data[index++],
                data[index++], data[index++], data[index++] }, 0);
        }

        public static string GetWString(byte[] data, ref int index)
        {
            // strings in Skyrim save files are proceeded by a uint16
            // which is the length of the string. they are not null-terminated
            ushort stringLength = BitConverter.ToUInt16(new byte[] { data[index++],
                    data[index++] }, 0);

            byte[] byteList = new byte[stringLength];
            for (int i = 0; i < stringLength; i++)
            {
                byteList[i] = data[index++];
            }

            return Encoding.UTF8.GetString(byteList);
        }

        public static RefID ReadRefID(ref FileStream fileStream)
        {
            return new RefID((byte)fileStream.ReadByte(),
                             (byte)fileStream.ReadByte(),
                             (byte)fileStream.ReadByte());
        }

        public static RefID GetRefID(byte[] data, ref int index)
        {
            return
                new RefID(data[index++], data[index++], data[index++]);
        }

        public static VSVal ReadVSVal(ref FileStream fileStream)
        {
            byte byte0 = (byte)fileStream.ReadByte();
            byte mask = (1 << 2) - 1;
            byte valueType = (byte)(byte0 & mask);

            byte uint8Value;
            ushort uint16Value;
            uint uint32Value;

            switch((VSVal.ValType) valueType)
            {
                case VSVal.ValType.UInt8:
                    uint8Value = (byte)(byte0 >> 2);
                    return new VSVal(uint8Value);

                case VSVal.ValType.UInt16:
                    byte byte1 = (byte)fileStream.ReadByte();
                    uint16Value = (ushort)((byte0 | byte1 << 8) >> 2);
                    return new VSVal(uint16Value);

                case VSVal.ValType.UInt32:
                    byte byte2 = (byte)fileStream.ReadByte();
                    byte byte3 = (byte)fileStream.ReadByte();
                    uint32Value =
                        (uint)((byte0 | (byte2 << 8) | (byte3) << 16) >> 2);
                    return new VSVal(uint32Value);

                default:
                    throw new Exception("Unexpected VSVal value.");
            }

        }

        public static VSVal GetVSVal(byte[] data, ref int index)
        {
            byte byte0 = data[index++];

            byte mask = (1 << 2) - 1;
            byte valueType = (byte)(byte0 & mask);

            byte uint8Value;
            ushort uint16Value;
            uint uint32Value;

            switch ((VSVal.ValType) valueType)
            {
                case VSVal.ValType.UInt8:
                    uint8Value = (byte)(byte0 >> 2);
                    return new VSVal(uint8Value);

                case VSVal.ValType.UInt16:
                    uint16Value = (ushort)((byte0 | (data[index++] << 8)) >> 2);
                    return new VSVal(uint16Value);

                case VSVal.ValType.UInt32:
                    uint32Value =
                        (uint)((byte0 | (data[index++] << 8) | (data[index++]) << 16) >> 2);
                    return new VSVal(uint32Value);

                default:
                    throw new Exception("Unexpected VSVal value.");
            }
        }
    }
}
