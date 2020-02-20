using System;

namespace SkySaves.GlobalDataTable1
{
    public class SkyCellsTable
    {
        public struct Unknown0
        {
            public readonly RefID Unknown1;
            public readonly RefID Unknown2;

            public Unknown0(RefID unknown1, RefID unknown2)
            {
                Unknown1 = unknown1;
                Unknown2 = unknown2;
            }
        }

        public VSVal Count { get; private set; }
        public Unknown0[] Unknown { get; private set; }

        public SkyCellsTable(byte[] data)
        {
            int index = 0;

            Count = BinHelp.GetVSVal(data, ref index);
            int count = Count.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            Unknown = new Unknown0[count];

            for (int i = 0; i < count; i++)
            {
                RefID unk1 = BinHelp.GetRefID(data, ref index);
                RefID unk2 = BinHelp.GetRefID(data, ref index);
                Unknown[i] = new Unknown0(unk1, unk2);
            }
        }

    }
}
