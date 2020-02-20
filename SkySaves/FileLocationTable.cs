using System;
using System.IO;

namespace SkySaves
{
    public class FileLocationTable
    {
        public uint FormIDArrayCountOffset { get; private set; }
        public uint UnknownTable3Offset { get; private set; }
        public uint GlobalDataTable1Offset { get; private set; }
        public uint GlobalDataTable2Offset { get; private set; }
        public uint ChangeFormsOffset { get; private set; }
        public uint GlobalDataTable3Offset { get; private set; }
        public uint GlobalDataTabel1Count { get; private set; }
        public uint GlobalDataTabel2Count { get; private set; }
        public uint GlobalDataTabel3Count { get; private set; }
        public uint ChangeFormCount { get; private set; }
        public uint[] Unused { get; private set; }

        public FileLocationTable(ref FileStream fileStream)
        {
            FormIDArrayCountOffset = BinHelp.ReadUInt32(ref fileStream);
            UnknownTable3Offset = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTable1Offset = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTable2Offset = BinHelp.ReadUInt32(ref fileStream);
            ChangeFormsOffset = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTable3Offset = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTabel1Count = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTabel2Count = BinHelp.ReadUInt32(ref fileStream);
            GlobalDataTabel3Count = BinHelp.ReadUInt32(ref fileStream);
            ChangeFormCount = BinHelp.ReadUInt32(ref fileStream);

            Unused = new uint[15];
            for (int i = 0; i < Unused.Length; i++)
                Unused[i] = BinHelp.ReadUInt32(ref fileStream);
        }
}
}
