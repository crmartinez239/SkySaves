using System;

namespace SkySaves
{
    public class PlayerLocationTable
    {
        public uint NextObjectID { get; private set; }
        public RefID WorldSpace1 { get; private set; }
        public int CoorX { get; private set; }
        public int CoorY { get; private set; }
        public RefID WorldSpace2 { get; private set; }
        public float PosX { get; private set; }
        public float PosY { get; private set; }
        public float PosZ { get; private set; }
        public byte Unknown { get; private set; } //absent in 9th version

        public PlayerLocationTable(byte[] data)
        {
            int index = 0;

            NextObjectID = BinHelp.GetUInt32(data, ref index);
            WorldSpace1 = BinHelp.GetRefID(data, ref index);
            CoorX = BinHelp.GetInt32(data, ref index);
            CoorY = BinHelp.GetInt32(data, ref index);
            WorldSpace2 = BinHelp.GetRefID(data, ref index);
            PosX = BinHelp.GetFloat(data, ref index);
            PosY = BinHelp.GetFloat(data, ref index);
            PosZ = BinHelp.GetFloat(data, ref index);
            Unknown = data[index];
        }

    }
}
