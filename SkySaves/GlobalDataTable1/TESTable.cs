using System;

namespace SkySaves
{
    public class TESTable
    {
        public struct Unknown0
        {
            public readonly RefID FormID;
            public readonly ushort Unknown;

            public Unknown0(RefID formId, ushort unknown)
            {
                FormID = formId;
                Unknown = unknown;
            }
        }

        public VSVal Count1 { get; private set; }
        public Unknown0[] Unknown1 { get; private set; }
        public uint Count2 { get; private set; }
        public RefID[] Unknown2 { get; private set; }
        public VSVal Count3 { get; private set; }
        public RefID[] Unknown3 { get; private set; }

        public TESTable(byte[] data)
        {
            int index = 0;

            Count1 = BinHelp.GetVSVal(data, ref index);
            int count1Value = Count1.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            Unknown1 = new Unknown0[count1Value];

            for (int i = 0; i < count1Value; i++)
            {
                RefID formID = BinHelp.GetRefID(data, ref index);
                ushort unknown = BinHelp.GetUInt16(data, ref index);
                Unknown1[i] = new Unknown0(formID, unknown);
            }

            Count2 = BinHelp.GetUInt32(data, ref index);
            Unknown2 = new RefID[Count2 * Count2];

            for (uint i = 0; i < Count2 * Count2; i++)
                Unknown2[i] = BinHelp.GetRefID(data, ref index);

            Count3 = BinHelp.GetVSVal(data, ref index);
            int count3Value = Count3.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            Unknown3 = new RefID[count3Value];

            for (int i = 0; i < count3Value; i++)
                Unknown3[i] = BinHelp.GetRefID(data, ref index);

        }
    
    }
}
